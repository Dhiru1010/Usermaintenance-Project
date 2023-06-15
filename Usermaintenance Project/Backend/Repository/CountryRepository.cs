using Domain3.Model;
using UsermaintenanceUsingWebApi.Models;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Repository
{
    public class CountryRepository : ICountryRepository
    {

        private readonly DhiruDBUserMaintenanceWebContext _dbContext;
        public CountryRepository(DhiruDBUserMaintenanceWebContext userMainDBContext)
        {
            _dbContext = userMainDBContext;
        }
        public List<GICountryViewModel> GetAllActiveCountries()
        {
            var CountryList = _dbContext.TblGicountries.Where(x => x.CountryStatus == true).Select(x => new GICountryViewModel { CountryId = x.CountryId, Name = x.CountryName, Status = x.CountryStatus }).ToList();
            return CountryList;
           
        }

        public List<GICountryViewModel> GetAllCountries()
        {
            return _dbContext.TblGicountries.Select(x =>
            new GICountryViewModel { CountryId = x.CountryId, Name = x.CountryName ?? "", Status = x.CountryStatus })
                .ToList();
        }

        public void ChangeCountryStatus(int countryId)
        {
            var country = _dbContext.TblGicountries.Where(x => x.CountryId == countryId).FirstOrDefault();
            if (country != null)
            {
                country.CountryStatus = country.CountryStatus == true ? false : true;
                _dbContext.SaveChanges();
            }
        }

        public void ChangeCountryStatusbyvalue(string name, bool status)
        {
            var country = _dbContext.TblGicountries.Where(x => x.CountryName == name).FirstOrDefault();
            if (country != null)
            {
                country.CountryStatus = status;
                _dbContext.SaveChanges();
            }
        }

        public PaginationCountryViewModel GetAllCountriesByPagination(int skip, int take)
        {

            var total_Country =  _dbContext.TblGicountries.OrderBy(r => r.CountryId).Skip(skip).Take(take).
                Select(user => new GICountryViewModel
                {
                    CountryId = user.CountryId,
                    Name = user.CountryName?? "",
                   
                    Status = user.CountryStatus
                }).ToList();
            var total_Country_Count = _dbContext.TblGicountries.Count();

            return new PaginationCountryViewModel { CountryList = total_Country, totalCountryCounts = total_Country_Count };
        }

        public void AddCountry(string country)
        {
            var newCountry = new TblGicountry
            {
          
                CountryName = country,
                CountryStatus = true
            };
            _dbContext.TblGicountries.Add(newCountry);
            _dbContext.SaveChanges();
        }

        public void EditCountry(int countryId, GICountryViewModel country)
        {
            var countrycheck = _dbContext.TblGicountries.Where(x => x.CountryId==countryId).FirstOrDefault();
            if(countrycheck != null)
            {
                countrycheck.CountryName = country.Name;
                countrycheck.CountryStatus = country.Status;
                

                _dbContext.SaveChanges();
            }
        }

        public Boolean IsCountryExists(string name)
        {
            return _dbContext.TblGicountries.Any(x=>x.CountryName==name);
        }

        public string GetCountryName(int countryId)
        {
            return _dbContext.TblGicountries.Where(x => x.CountryId == countryId).Select(x => x.CountryName).FirstOrDefault();
        }

    }

    public interface ICountryRepository
    {
        public List<GICountryViewModel> GetAllActiveCountries();

        public List<GICountryViewModel> GetAllCountries();

        public void ChangeCountryStatus(int countryId);

        public PaginationCountryViewModel GetAllCountriesByPagination(int skip, int take);

        public void AddCountry(string country);

        public void EditCountry(int countryId, GICountryViewModel country);

        public Boolean IsCountryExists(string name);

        public string GetCountryName(int countryId);

        public void ChangeCountryStatusbyvalue(string name, bool status);
    }
}
