using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsermaintenanceUsingWebApi.Repository;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly ICityRepository _cityRepository;

        private readonly ICountryRepository _countryRepository;

        private readonly IStateRepository _stateRepository;

        private readonly IUserSQRepository _userSQRepository;

        public ManageController(IUserRepository userRepository, ICityRepository cityRepository, ICountryRepository countryRepository, IStateRepository stateRepository, IUserSQRepository userSQRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _userSQRepository = userSQRepository;
        }

        [HttpGet]
        [Route("GetAllCountries")]
        public List<GICountryViewModel> GetAllCountries()
        {
            return _countryRepository.GetAllCountries();
        }

      

        [HttpPost]
        [Route("ChangeCountryStatus")]
        public void ChangeCountryStatus(int countryId)
        {
            _countryRepository.ChangeCountryStatus(countryId);
        }

        [HttpPost]
        [Route("ChangeCountryStatusbyvalue")]
        public void ChangeCountryStatusbyvalue(string country,bool status)
        {
            _countryRepository.ChangeCountryStatusbyvalue(country, status);
        }

        [HttpPost]
        [Route("AddCountries")]
        public void AddCountries(string country)
        {
            _countryRepository.AddCountry(country);
        }

        [HttpPost]
        [Route("EditCountry")]
        public void EditCountry(int countryId,[FromForm] GICountryViewModel country)
        {
            _countryRepository.EditCountry(countryId, country);
        }

        [HttpPost]
        [Route("IsCountryExists")]
        public bool IsCountryExists(string countryname)
        {
            return _countryRepository.IsCountryExists(countryname);
        }



        [HttpPost]
        [Route("GetCountryName")]
        public string GetCountryName(int countryId)
        {
            return _countryRepository.GetCountryName(countryId);
        }




        //states 

        [HttpGet]
        [Route("GetAllStatesByCountryId")]
        public List<GIStateViewModel> GetAllStatesByCountryId(int countryId)
        {
            return _stateRepository.GetAllStatesByCountryId(countryId);
        }

        [HttpPost]
        [Route("AddState")]
        public void AddState(string state,int countryId)
        {
             _stateRepository.AddState(state,countryId);
        }

        [HttpPost]
        [Route("ChangeStateStatus")]
        public void ChangeStateStatus(string state, bool status)
        {
            _stateRepository.ChangeStateStatus(state, status);
        }

        [HttpPost]
        [Route("ChangeStateStatusById")]
        public void ChangeStateStatusById(int stateId)
        {
            _stateRepository.ChangeStateStatusById(stateId);
        }

        [HttpPost]
        [Route("EditState/{stateId}")]
        public void EditState([FromRoute] int stateId, GIStateViewModel state)
        {
            _stateRepository.EditState(stateId, state);
        }

        [HttpGet]
        [Route("GetAllActiveStatesByCountryListId")]
        public IQueryable<GIStateViewModel> GetAllActiveStatesByCountryListId(int countryId)
        {
            return _stateRepository.GetAllActiveStatesByCountryListId(countryId);
        }

        [HttpGet]
        [Route("GetAllStatesByPagination")]
        public PaginationStatesViewModel GetAllStatesByPagination(int skip,int take,int countryId)
        {
            return _stateRepository.GetAllStatesByPagination(skip, take, countryId);
        }

        [HttpPost]
        [Route("GetStateName")]
        public string GetStateName(int stateId)
        {
            return _stateRepository.GetStateName(stateId);
        }

        [HttpPost]
        [Route("IsStateExists")]
        public bool IsStateExists(string state,int countryId)
        {
            return _stateRepository.IsStateExists(state,countryId);
        }



        //city

        [HttpGet]
        [Route("GetAllCitiesByStateId")]
        public List<GICityViewModel> GetAllCitiesByStateId(int stateId)
        {
            return _cityRepository.GetAllCitiesByStateId(stateId);
        }

        [HttpPost]
        [Route("AddCity")]
        public void AddCity(string city, int state)
        {
            _cityRepository.AddCity(city, state);
        }

        [HttpPost]
        [Route("ChangeCityStatus")]
        public void ChangeCityStatus(int cityId)
        {
            _cityRepository.ChangeCityStatus(cityId);
        }

        [HttpPost]
        [Route("EditCity")]
        public void EditCity([FromRoute] GICityViewModel city)
        {
            _cityRepository.EditCity(city);
        }


        [HttpPost]
        [Route("GetAllCitiesByPagination")]
        public PaginationCitiesViewModel GetAllCitiesByPagination(int skip, int take, int stateId)
        {
            return _cityRepository.GetAllCitiesByPagination(skip, take, stateId);
        }

        [HttpPost]
        [Route("GetCityName")]
        public string GetCityName(int cityId)
        {
            return _cityRepository.GetCityName(cityId);
        }

        [HttpPost]
        [Route("IsCityExists")]
        public bool IsCityExists(string city,int stateId)
        {
            return _cityRepository.IsCityExists(city,stateId);
        }

        [HttpPost]
        [Route("ChangeCityStatusByName")]
        public void ChangeCityStatusByName(string name, bool status)
        {
            _cityRepository.ChangeCityStatusByName(name, status);
        }
    }
}
