using System.ComponentModel.DataAnnotations.Schema;

namespace AgileProject.Model
{
    public class Usuario
    {
        public string? Id_Usuario { get; set; }
        public string? UserName { get; set; }
        public string? Clave { get; set; }

        public int UserId { get; set; }
        public string? Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }

        public bool? Estado { get; set; }

        [NotMapped]
        public bool MantenerActivo { get; set; }
    }
}

