using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsermaintenanceUsingWebApi.Repository;

namespace UsermaintenanceUsingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IUserRepository _userRepository;


        public PasswordController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
           
        }
        [HttpGet]
        [Route("ForgotPassword")]
        public string ForgotPassword(string userId, string SQ, string SA)
        {
            var x  = _userRepository.ForgetPassword(userId, SQ, SA);
            return x;
        }

      
    }
}
