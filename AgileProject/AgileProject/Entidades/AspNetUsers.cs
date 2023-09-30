using System;

namespace AgileProject.Entidades
{
    public class AspNetUsers 
    {
        public string Id { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}

