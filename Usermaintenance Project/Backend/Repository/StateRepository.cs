using Domain3.Model;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using UsermaintenanceUsingWebApi.ViewModels;

namespace UsermaintenanceUsingWebApi.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly DhiruDBUserMaintenanceWebContext _dbContext;
        public StateRepository(DhiruDBUserMaintenanceWebContext userMainDBContext)
        {
            _dbContext = userMainDBContext;
        }

        public void AddState(string state,int countryId)
        {
            var newState= new TblGistate
            {
                StateName = state,
                StateStatus = true,
                CountryId= countryId
            };
            _dbContext.TblGistates.Add(newState);
            _dbContext.SaveChanges();
        }

        public void ChangeStateStatus(string state, bool status)
        {
            var states = _dbContext.TblGistates.Where(x => x.StateName == state).FirstOrDefault();
            if (states != null)
            {
                states.StateStatus = status;
                _dbContext.SaveChanges();
            }
        }
        public void ChangeStateStatusById(int stateId)
        {
            var state = _dbContext.TblGistates.Where(x => x.StateId == stateId).FirstOrDefault();
            if (state != null)
            {
                state.StateStatus = state.StateStatus == true ? false : true;
                _dbContext.SaveChanges();
            }
        }

        public void EditState(int stateId, GIStateViewModel state)
        {
            var statecheck = _dbContext.TblGistates.Where(x => x.StateId == stateId).FirstOrDefault();
            if (statecheck != null)
            {
                statecheck.StateName = state.Name;
                statecheck.StateStatus = state.Status;
                statecheck.StateId = stateId;
                _dbContext.SaveChanges();
            }
        }

        public List<GIStateViewModel> GetAllActiveStatesByCountryId(int countryId)
        {
            var TotalStates = _dbContext.TblGistates
                .Where(x => (x.StateStatus == true) && (x.CountryId == countryId))
                .Select(x => new GIStateViewModel
                {
                    StateId = x.StateId,
                    Name = x.StateName ?? "",
                    Status = x.StateStatus
                }).ToList();

            return TotalStates;
        }

        public IQueryable<GIStateViewModel> GetAllActiveStatesByCountryListId(int countryId)
        {
            var TotalStates = _dbContext.TblGistates
                .Where(x => (x.StateStatus == true) && (x.CountryId == countryId))
                .Select(x => new GIStateViewModel
                {
                    StateId = x.StateId,
                    Name = x.StateName ?? "",
                    Status = x.StateStatus
                }).AsQueryable();
            return TotalStates;
        }

        public List<GIStateViewModel> GetAllStatesByCountryId(int countryId)
        {
            var TotalStates = _dbContext.TblGistates
                .Where(x => (x.CountryId == countryId))
                .Select(x => new GIStateViewModel
                {
                    StateId = x.StateId,
                    Name = x.StateName ?? "",
                    Status = x.StateStatus
                }).ToList();
            return TotalStates;
        }

        public PaginationStatesViewModel GetAllStatesByPagination(int skip, int take,int countryId)
        {
            var StateId = _dbContext.TblGistates.Where(x => x.CountryId == countryId).ToList();
            var TotalStates = StateId.OrderBy(y => y.StateId).Skip(skip).Take(take).Where(x=> x.CountryId == countryId).Select(x => new GIStateViewModel
               {
                   StateId = x.StateId,
                   Name = x.StateName ?? "",
                   Status = x.StateStatus
               }).ToList();
             var totalStateCount = _dbContext.TblGistates.Count(x=>x.CountryId == countryId);

            return new PaginationStatesViewModel { State = TotalStates, totalStates =  totalStateCount };
        }

        public string GetStateName(int stateId)
        {
            return _dbContext.TblGistates.Where(x => x.StateId == stateId).Select(x => x.StateName).FirstOrDefault();
        }

        public bool IsStateExists(string name,int countryId)
        {
            return _dbContext.TblGistates.Any(x => x.StateName== name && x.CountryId == countryId);
        }
    }
    public interface IStateRepository
    {
        public List<GIStateViewModel> GetAllActiveStatesByCountryId(int countryId);
        public IQueryable<GIStateViewModel> GetAllActiveStatesByCountryListId(int countryId);

        public List<GIStateViewModel> GetAllStatesByCountryId(int countryId);

        public void ChangeStateStatus(string state, bool status);

        public PaginationStatesViewModel GetAllStatesByPagination(int skip, int take,int countryId);

        public void AddState(string state,int countryId);

        public void EditState(int stateId, GIStateViewModel state);

        public Boolean IsStateExists(string name, int countryId);

        public string GetStateName(int stateId);

        public void ChangeStateStatusById(int stateId);
    }
}
