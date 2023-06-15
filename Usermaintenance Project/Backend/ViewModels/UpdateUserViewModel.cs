namespace UsermaintenanceUsingWebApi.ViewModels
{
    public class UpdateUserViewModel
    {
        public string UserId { get; set; } = "";
        public string? Password { get; set; }
        public string? UserSQ { get; set; }
        public string? UserSA { get; set; }
        public string? UserType { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Status { get; set; }
        public string? Name { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string? PinCode { get; set; }
        public string? Mobile { get; set; }
    }
}
