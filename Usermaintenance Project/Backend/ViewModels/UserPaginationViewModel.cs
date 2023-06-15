using UsermaintenanceUsingWebApi.Models;

namespace UsermaintenanceUsingWebApi.ViewModels
{
    public class UserPaginationViewModel
    {
        public List<UserViewModel>? users { get; set; }
        public int TotalRecords { get; set; }
    }
}
