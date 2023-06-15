using Domain3.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using UsermaintenanceUsingWebApi.Models;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DhiruDBUserMaintenanceWebContext _dbContext;
        public UserRepository(DhiruDBUserMaintenanceWebContext userMainDBContext)
        {
            _dbContext = userMainDBContext;
        }

        public bool IsUserEmailExists(string email)
        {
            return _dbContext.TblUserDetails.Any(x => x.UsrEmail == email);

            //var check = _dbContext.TblUserDetails.Where(x => x.UsrEmail == email).Select(x => x).ToList();
            //if (check != null)
            //{
            //    return true;

            //}
            //else
            //{
            //    return false;
            //}
        }

        public bool IsUserIdExists(string userId)
        {
            return _dbContext.TblUsers.Any(x => x.UsrId == userId);
        }

        public bool IsUserMobileExists(string mobile)
        {
            return _dbContext.TblUserDetails.Any(x => x.UsrMobile == mobile);
        }

        public void SignUpUser(UserViewModel user)
        {
            TblUser tblUser = new()
            {
                UsrId = user.UserId,
                UsrPwd = user.Password,
                UsrSq = user.UserSQ,
                UsrSa = user.UserSA,
                UsrType = "user",
                UsrLastLogin = null,
                UsrStatus = "active",
                TblUserDetail = new TblUserDetail()
                {
                    UsrName = user.Name,
                    UsrDob = user.DOB,
                    UsrGender = user.Gender,
                    UsrEmail = user.Email,
                    UsrStreet = user.Street,
                    UsrCountry = user.Country,
                    UsrState = user.State,
                    UsrCity = user.City,
                    UsrPin = user.PinCode,
                    UsrMobile = user.Mobile,
                }
            };

            _dbContext.TblUsers.Add(tblUser);
            _dbContext.SaveChanges();

        }

        public UserViewModel? UserLogin(string UserId, string Password)
        {
            var user = _dbContext.TblUsers.Where(x => x.UsrId.ToLower() == UserId.ToLower() && x.UsrPwd == Password && (x.UsrStatus.ToLower() == "active")).FirstOrDefault();
            if (user != null)
            {
                var userDetail = _dbContext.TblUserDetails.Where(x => x.UsrId == UserId).FirstOrDefault();

                UserViewModel UserViewDetail = new()
                {
                    UserId = user.UsrId,
                    Password = user.UsrPwd,
                    UserSQ = user.UsrSq,
                    UserSA = user.UsrSa,
                    UserType = user.UsrType,
                    LastLogin = user.UsrLastLogin,
                    Status = user.UsrStatus,
                    Name = userDetail.UsrName,
                    DOB = userDetail.UsrDob,
                    Gender = userDetail.UsrGender,
                    Email = userDetail.UsrEmail,
                    Street = userDetail.UsrStreet,
                    Country = userDetail.UsrCountry,
                    State = userDetail.UsrState,
                    City = userDetail.UsrCity,
                    PinCode = userDetail.UsrPin,
                    Mobile = userDetail.UsrMobile
                };

                user.UsrLastLogin = DateTime.UtcNow;
                _dbContext.SaveChanges();

                return UserViewDetail;
            }
            return null;
        }

        public UserViewModel? ThirdUserLogin(string UserId, string Password)
        {
            //var user = _dbContext.TblUsers.Where(x => x.UsrId.ToLower() == UserId.ToLower() && x.UsrPwd == Password && (x.UsrStatus.ToLower() == "active")).FirstOrDefault();
            var user = _dbContext.TblUserDetails.Include(x => x.Usr).Where(x => x.UsrId.ToLower() == UserId.ToLower() &&
                                                   x.Usr.UsrPwd == Password && x.Usr.UsrStatus == "active").FirstOrDefault();

            if (user == null)
            {
                return null;
            }


            UserViewModel UserViewDetail = new()
            {
                UserId = user.UsrId,
                Password = user.Usr.UsrPwd,
                UserSQ = user.Usr.UsrSq,
                UserSA = user.Usr.UsrSa,
                UserType = user.Usr.UsrType,
                LastLogin = user.Usr.UsrLastLogin,
                Status = user.Usr.UsrStatus,
                Name = user.UsrName,
                DOB = user.UsrDob,
                Gender = user.UsrGender,
                Email = user.UsrEmail,
                Street = user.UsrStreet,
                Country = user.UsrCountry,
                State = user.UsrState,
                City = user.UsrCity,
                PinCode = user.UsrPin,
                Mobile = user.UsrMobile
            };

            user.Usr.UsrLastLogin = DateTime.UtcNow;
            _dbContext.SaveChanges();

            return UserViewDetail;

        }



        public string SecondUserLogin(string UserId, string Password)
        {
            var user = _dbContext.TblUsers.Where(x => x.UsrId.ToLower() == UserId.ToLower()).FirstOrDefault();
            if (user != null)
            {
                if (user.UsrPwd == Password)
                {
                    if (user.UsrStatus == "active")
                    {
                        return "User Login Successfully";
                    }
                    else
                    {
                        return "User is Not Acitve";
                    }
                }
                else
                {
                    return "Password is not Valid";
                }

            }
            else
            {
                return "Invalide UserId";
            }
        }

        //Th
        public List<UserViewModel> GetAllUsers()
        {

            return _dbContext.TblUsers.Join(_dbContext.TblUserDetails, u => u.UsrId, uir => uir.UsrId, (u, uir) => new { u, uir }).
                Select(user => new UserViewModel
                {
                    UserId = user.u.UsrId,
                    Password = user.u.UsrPwd,
                    UserSQ = user.u.UsrSq,
                    UserSA = user.u.UsrSa,
                    UserType = user.u.UsrType,
                    LastLogin = user.u.UsrLastLogin,
                    Status = user.u.UsrStatus,
                    Name = user.uir.UsrName,
                    DOB = user.uir.UsrDob,
                    Gender = user.uir.UsrGender,
                    Email = user.uir.UsrEmail,
                    Street = user.uir.UsrStreet,
                    Country = user.uir.UsrCountry,
                    State = user.uir.UsrState,
                    City = user.uir.UsrCity,
                    PinCode = user.uir.UsrPin,
                    Mobile = user.uir.UsrMobile

                }).ToList();
        }

        public void ChangeUserStatus(string userId)
        {
            var user = _dbContext.TblUsers.Where(u => u.UsrId == userId).Select(x => x).FirstOrDefault();
            user.UsrStatus = user.UsrStatus == "active" ? "inactive" : "active";
            _dbContext.SaveChanges();
        }

        public UserPaginationViewModel UserPagination(int skip, int take)
        {
            var totalUsers =  _dbContext.TblUsers.Join(_dbContext.TblUserDetails, u => u.UsrId, uir => uir.UsrId, (u, uir) => new { u, uir }).OrderBy(r => r.u.UsrId).Skip(skip).Take(take).
                Select(user => new UserViewModel
                {
                    UserId = user.u.UsrId,
                    Password = user.u.UsrPwd,
                    UserSQ = user.u.UsrSq,
                    UserSA = user.u.UsrSa,
                    UserType = user.u.UsrType,
                    LastLogin = user.u.UsrLastLogin,
                    Status = user.u.UsrStatus,
                    Name = user.uir.UsrName,
                    DOB = user.uir.UsrDob,
                    Gender = user.uir.UsrGender,
                    Email = user.uir.UsrEmail,
                    Street = user.uir.UsrStreet,
                    Country = user.uir.UsrCountry,
                    State = user.uir.UsrState,
                    City = user.uir.UsrCity,
                    PinCode = user.uir.UsrPin,
                    Mobile = user.uir.UsrMobile

                }).ToList();

            var totalUsersCount = _dbContext.TblUsers.Count();
            UserPaginationViewModel paginationViewModels = new()
            {
                TotalRecords = totalUsersCount,
                users = totalUsers
            };
            return paginationViewModels; 
        }

        public List<UserViewModel> UserSearch(string userId)
        {
            return _dbContext.TblUsers.Join(_dbContext.TblUserDetails, u => u.UsrId, uir => uir.UsrId, (u, uir) => new { u, uir }).Where(x => (x.u.UsrId.Contains(userId)) ||
            (x.u.UsrId.StartsWith(userId.Substring(0))) || (x.u.UsrId.EndsWith(userId.ToLower().Substring(userId.Length)))).Select(user => new UserViewModel
            {
                UserId = user.u.UsrId,
                Password = user.u.UsrPwd,
                UserSQ = user.u.UsrSq,
                UserSA = user.u.UsrSa,
                UserType = user.u.UsrType,
                LastLogin = user.u.UsrLastLogin,
                Status = user.u.UsrStatus,
                Name = user.uir.UsrName,
                DOB = user.uir.UsrDob,
                Gender = user.uir.UsrGender,
                Email = user.uir.UsrEmail,
                Street = user.uir.UsrStreet,
                Country = user.uir.UsrCountry,
                State = user.uir.UsrState,
                City = user.uir.UsrCity,
                PinCode = user.uir.UsrPin,
                Mobile = user.uir.UsrMobile

            }).ToList(); ;
        }

        public UserViewModel? GetUserInfoByUserId(string UserId)
        {
            var user = _dbContext.TblUsers.Join(_dbContext.TblUserDetails, userTable => userTable.UsrId, userDetail => userDetail.UsrId, (userTable, userDetail) => new { userTable, userDetail }).
                Where(x => x.userTable.UsrId.ToLower() == UserId.ToLower()).FirstOrDefault();
            if (user != null)
            {
                var userDetail = _dbContext.TblUserDetails.Where(x => x.UsrId == UserId).Select(x => new UserViewModel()
                {
                    UserId = user.userTable.UsrId,
                    Password = user.userTable.UsrPwd,
                    UserSQ = user.userTable.UsrSq,
                    UserSA = user.userTable.UsrSa,
                    UserType = user.userTable.UsrType,
                    LastLogin = user.userTable.UsrLastLogin,
                    Status = user.userTable.UsrStatus,
                    Name = user.userDetail.UsrName,
                    DOB = user.userDetail.UsrDob,
                    Gender = user.userDetail.UsrGender,
                    Email = user.userDetail.UsrEmail,
                    Street = user.userDetail.UsrStreet,
                    Country = user.userDetail.UsrCountry,
                    State = user.userDetail.UsrState,
                    City = user.userDetail.UsrCity,
                    PinCode = user.userDetail.UsrPin,
                    Mobile = user.userDetail.UsrMobile
                }).FirstOrDefault();


                return userDetail;
            }

            return null;

        }

        public void UpdateUserInfoByUserId(UserViewModel user)
        {
            var firstTable = _dbContext.TblUsers.Where(u => u.UsrId == user.UserId).FirstOrDefault();
            if (firstTable != null)
            {
                firstTable.UsrSq = user.UserSQ;
                firstTable.UsrSa = user.UserSA;


                var secondTable = _dbContext.TblUserDetails.Where(u => u.UsrId == user.UserId).FirstOrDefault();
                secondTable.UsrName = user.Name;
                secondTable.UsrDob = user.DOB;
                secondTable.UsrGender = user.Gender;
                secondTable.UsrEmail = user.Email;
                secondTable.UsrStreet = user.Street;
                secondTable.UsrCountry = user.Country;
                secondTable.UsrState = user.State;
                secondTable.UsrCity = user.City;
                secondTable.UsrPin = user.PinCode;
                secondTable.UsrMobile = user.Mobile;

                _dbContext.SaveChanges();
            }

        }

        public void UpdatePassword(string userId, string currentPassword, string newPassword)
        {
            var firstTable = _dbContext.TblUsers.Where(u => u.UsrId == userId && u.UsrPwd == currentPassword).FirstOrDefault();
            if (firstTable != null)
            {
                firstTable.UsrPwd = newPassword;
                _dbContext.SaveChanges();
            }
        }

        public String ForgetPassword(string userId, string SQ, string SA)
        {
            var y = _dbContext.TblUsers.Where(x => (x.UsrId == userId) && (x.UsrSq == SQ && x.UsrSa == SA)).Select(x => x.UsrPwd).FirstOrDefault();
            return y;
        }

        public UpdateUserViewModel GetUpdateUserInfoByUserId(string UserId)
        {
            var user = _dbContext.TblUsers.Join(_dbContext.TblUserDetails, userTable => userTable.UsrId, userDetail => userDetail.UsrId, (userTable, userDetail) => new { userTable, userDetail }).
               Where(x => x.userTable.UsrId.ToLower() == UserId.ToLower()).FirstOrDefault();
            if (user != null)
            {
                var usercounty = _dbContext.TblGicountries.Where(x => x.CountryId == user.userDetail.UsrCountry).Select(x => x.CountryName).FirstOrDefault();
                var userstate = _dbContext.TblGistates.Where(x => x.StateId == user.userDetail.UsrState).Select(x => x.StateName).FirstOrDefault();
                var usercity = _dbContext.TblGicities.Where(x => x.CityId == user.userDetail.UsrCity).Select(x => x.CityName).FirstOrDefault();


                var userDetail = _dbContext.TblUserDetails.Where(x => x.UsrId == UserId).Select(x => new UpdateUserViewModel()
                {
                    UserId = user.userTable.UsrId,
                    Password = user.userTable.UsrPwd,
                    UserSQ = user.userTable.UsrSq,
                    UserSA = user.userTable.UsrSa,
                    UserType = user.userTable.UsrType,
                    LastLogin = user.userTable.UsrLastLogin,
                    Status = user.userTable.UsrStatus,
                    Name = user.userDetail.UsrName,
                    DOB = user.userDetail.UsrDob,
                    Gender = user.userDetail.UsrGender,
                    Email = user.userDetail.UsrEmail,
                    Street = user.userDetail.UsrStreet,
                    CountryId = user.userDetail.UsrCountry,
                    Country = usercounty,
                    StateId = user.userDetail.UsrState,
                    State = userstate,
                    CityId = user.userDetail.UsrCity,
                    City = usercity,
                    PinCode = user.userDetail.UsrPin,
                    Mobile = user.userDetail.UsrMobile
                }).FirstOrDefault();


                return userDetail;
            }
            else
            {
                return null;
            }
        }

      
    }

    public interface IUserRepository
    {
        public UserViewModel UserLogin(string UserId, string Password);

        public bool IsUserIdExists(string userId);

        public void SignUpUser(UserViewModel user);

        public bool IsUserEmailExists(string email);

        public bool IsUserMobileExists(string mobile);
        public List<UserViewModel> GetAllUsers();

        public void ChangeUserStatus(string userId);

        public UserPaginationViewModel UserPagination(int skip, int take);

        public List<UserViewModel> UserSearch(string userId);

        public void UpdateUserInfoByUserId(UserViewModel user);

        public UserViewModel GetUserInfoByUserId(string userId);

        public void UpdatePassword(string userId, string currentPassword, string newPassword);

        public String ForgetPassword(string userId, string SQ, string SA);

        public UpdateUserViewModel GetUpdateUserInfoByUserId(string UserId);

        public string SecondUserLogin(string UserId, string Password);

        
    }
}
