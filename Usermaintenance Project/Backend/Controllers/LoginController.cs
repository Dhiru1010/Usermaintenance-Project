using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Newtonsoft.Json;
using System.Collections;
using UsermaintenanceUsingWebApi.Models;
using UsermaintenanceUsingWebApi.Repository;

namespace UsermaintenanceUsingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("UserLogin")]
        [HttpPost]
        public UserViewModel Post(string username, string password)
        {
            return _userRepository.UserLogin(username, password);   
        }
        [HttpPost("SecondLogin")]
        public string SecondPost(string username, string password)
        {
            return _userRepository.SecondUserLogin(username, password);  
        }
    }
  



}
