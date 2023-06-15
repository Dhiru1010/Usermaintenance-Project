using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsermaintenanceUsingWebApi.Models;
using UsermaintenanceUsingWebApi.Repository;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly ICityRepository _cityRepository;

        private readonly ICountryRepository _countryRepository;

        private readonly IStateRepository _stateRepository;

        private readonly IUserSQRepository _userSQRepository;

        public SignUpController(IUserRepository userRepository, ICityRepository cityRepository, ICountryRepository countryRepository, IStateRepository stateRepository, IUserSQRepository userSQRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _userSQRepository = userSQRepository;
        }

        [HttpGet]
        [Route("GetAllUserSQ")]
        public List<UserSQViewModel> GetAllUserSQ()
        {
            return _userSQRepository.GetAllUserSQs();
        }

        [HttpGet]
        [Route("GetAllActiveCountry")]
        public List<GICountryViewModel> GetAllActiveCountry()
        {
            return _countryRepository.GetAllActiveCountries();
        }

        [HttpGet]
        [Route("GetAllActiveStatesByCountryId")]
        public List<GIStateViewModel> GetAllActiveStatesByCountryId(int countryId)
        {
            return _stateRepository.GetAllActiveStatesByCountryId(countryId);
        }


        [HttpGet]
        [Route("GetAllActiveCitiesByStateId")]
        public List<GICityViewModel> GetAllActiveCitiesByStateId(int StateID)
        {
            return _cityRepository.GetAllActiveCitiesByStateId(StateID);
        }


        [HttpGet]
        [Route("IsUserExists")]
        public Boolean IsUserExists(string userID)
        {
            return _userRepository.IsUserIdExists(userID);
        }

        [HttpPost]
        [Route("SignUpUser")]
        public void SignUpUser([FromBody] UserViewModel user)
        {
            _userRepository.SignUpUser(user);
         
        }

    }

}
