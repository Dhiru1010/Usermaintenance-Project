using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsermaintenanceUsingWebApi.Models;
using UsermaintenanceUsingWebApi.Repository;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserRepository _userRepository;


        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        [HttpPost]
        [Route("ChangePassword")]
        public void ChangePassword(string userId, string currentPassword, string newPassword)
        {
            _userRepository.UpdatePassword(userId, currentPassword, newPassword);
        }

        [HttpPost]
        [Route("UpdateUserData")]
        public void UpdateUserData([FromBody] UserViewModel user) {
            _userRepository.UpdateUserInfoByUserId(user);
        }

        [HttpGet]
        [Route("GetUserInfoByUserId")]
        public UpdateUserViewModel GetUserInfoByUserId(string user)
        {
            return _userRepository.GetUpdateUserInfoByUserId(user);
        }

      
    }
}
