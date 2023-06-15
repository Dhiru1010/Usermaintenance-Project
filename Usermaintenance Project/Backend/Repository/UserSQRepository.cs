using Domain3.Model;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Repository
{
    public class UserSQRepository : IUserSQRepository
    {
        private readonly DhiruDBUserMaintenanceWebContext _dbContext;
        public UserSQRepository(DhiruDBUserMaintenanceWebContext userMainDBContext)
        {
            _dbContext = userMainDBContext;
        }
        public List<UserSQViewModel> GetAllUserSQs()
        {
            var toatalUserSq = _dbContext.TblUserSqs
                                            .Select(x => new UserSQViewModel()
                                            {
                                                UserSQ = x.UsrSq
                                            }).ToList();
            return toatalUserSq;


            //var ToatalUserSq = _dbContext.TblUserSqs.Select(x => x.UsrSq).ToList();
            //if(ToatalUserSq != null)
            //{
            //    List<UserSQViewModel> userSQViewModel = new List<UserSQViewModel>();
            //    foreach(var items in ToatalUserSq)
            //    {
            //        UserSQViewModel user = new()
            //        {
            //            UserSQ = items
            //        };
            //        userSQViewModel.Add(user);

            //    }
            //    return userSQViewModel;
            //}
            //else
            //{
            //    throw new Exception("Question is not found");
            //}
        }
    }

    public interface IUserSQRepository
    {
        public List<UserSQViewModel> GetAllUserSQs();
    }
}
