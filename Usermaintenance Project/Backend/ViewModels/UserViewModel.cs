using System;

namespace UsermaintenanceUsingWebApi.Models
{
    public class UserViewModel
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
        public int? Country { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public string? PinCode { get; set; }
        public string? Mobile { get; set; }
    }
}
