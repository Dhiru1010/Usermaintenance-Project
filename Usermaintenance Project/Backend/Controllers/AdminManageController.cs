using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsermaintenanceUsingWebApi.Models;
using UsermaintenanceUsingWebApi.Repository;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManageController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly ICityRepository _cityRepository;

        private readonly ICountryRepository _countryRepository;

        private readonly IStateRepository _stateRepository;

        private readonly IUserSQRepository _userSQRepository;

        public AdminManageController(IUserRepository userRepository, ICityRepository cityRepository, ICountryRepository countryRepository, IStateRepository stateRepository, IUserSQRepository userSQRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _userSQRepository = userSQRepository;
        }

        [HttpGet("UserPagination")]
        public UserPaginationViewModel UserPagination(int skip, int take) {
            return _userRepository.UserPagination(skip, take);
        }

        [HttpPost("ChangeUserStatus")]
        public void ChangeUserStatus(string userId)
        {
            _userRepository.ChangeUserStatus(userId);
        }


        [HttpGet("GetAllCountriesByPagination")]
        public PaginationCountryViewModel GetAllCountriesByPagination(int skip, int take)
        {
            return _countryRepository.GetAllCountriesByPagination(skip, take);
        }

    }
}
