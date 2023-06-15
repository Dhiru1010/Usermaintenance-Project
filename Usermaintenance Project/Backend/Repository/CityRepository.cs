using Domain3.Model;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DhiruDBUserMaintenanceWebContext _dbContext;
        public CityRepository(DhiruDBUserMaintenanceWebContext userMainDBContext)
        {
            _dbContext = userMainDBContext;
        }

        public List<GICityViewModel> GetAllActiveCitiesByStateId(int StateID)
        {
            var TotalActiveCities = _dbContext.TblGicities.Where(x => (x.CityStatus == true) && (x.StateId == StateID))
                .Select(x => new GICityViewModel()
                {
                    CityId = x.CityId,
                    Name = x.CityName,
                    Status = x.CityStatus
                }).ToList();
            return TotalActiveCities;



        }

        public List<GICityViewModel> GetAllCitiesByStateId(int StateID)
        {
            var TotalActiveCities = _dbContext.TblGicities.Where(x => (x.StateId == StateID))
                .Select(x => new GICityViewModel()
                {
                    CityId = x.CityId,
                    Name = x.CityName,
                    Status = x.CityStatus
                }).ToList();
            return TotalActiveCities;


        }

        public void ChangeCityStatus(int cityId)
        {
            var city = _dbContext.TblGicities.Where(x => x.CityId== cityId).FirstOrDefault();
            if (city != null)
            {
                city.CityStatus= city.CityStatus == true ? false : true;
                _dbContext.SaveChanges();
            }
        }

        public PaginationCitiesViewModel GetAllCitiesByPagination(int skip, int take, int stateId)
        {
            var citybystateId = _dbContext.TblGicities.Where(x => x.StateId == stateId).Select(x => x).ToList();
            var totalcity = citybystateId.OrderBy(r => r.CityId).Skip(skip).Take(take).Where(x=>x.StateId == stateId).
                Select(user => new GICityViewModel
                {
                    CityId = user.CityId,
                    Name = user.CityName,
                    Status = user.CityStatus,
                }).ToList();
            var totalCityCount = citybystateId.Count();
            return new PaginationCitiesViewModel { Cities = totalcity, TotalCitiesCount = totalCityCount };
        }

        public void AddCity(string city,int stateId)
        {
            var newCity= new TblGicity
            {
                CityName = city,
                CityStatus= true,
                StateId= stateId,
            };
            _dbContext.TblGicities.Add(newCity);
            _dbContext.SaveChanges();
        }

        public void EditCity(GICityViewModel city)
        {
            var citycheck = _dbContext.TblGicities.Where(x => x.CityId == city.CityId).FirstOrDefault();
            if (citycheck != null)
            {
                citycheck.CityName= city.Name;
                citycheck.CityStatus = city.Status;

                _dbContext.SaveChanges();
            }
        }

        public Boolean IsCityExists(string name,int stateId)
        {
            return _dbContext.TblGicities.Any(x => x.CityName== name && x.StateId == stateId);
        }

        public string GetCityName(int cityId)
        {
            return _dbContext.TblGicities.Where(x => x.CityId== cityId).Select(x => x.CityName).FirstOrDefault();
        }

        public void ChangeCityStatusByName(string name ,bool status)
        {
            var city = _dbContext.TblGicities.Where(x => x.CityName== name).FirstOrDefault();
            if (city != null)
            {
                city.CityStatus= status;
                _dbContext.SaveChanges();
            }
        }

    }


    public interface ICityRepository
    {
        public List<GICityViewModel> GetAllActiveCitiesByStateId(int StateID);
        public List<GICityViewModel> GetAllCitiesByStateId(int StateID);

        public void ChangeCityStatus(int cityId);

        public PaginationCitiesViewModel GetAllCitiesByPagination(int skip, int take, int stateId);

        public void AddCity(string city, int state);

        public void EditCity(GICityViewModel city);

        public string GetCityName(int cityId);

        public Boolean IsCityExists(string name,int stateId);

        public void ChangeCityStatusByName(string name, bool status);


    }
}
