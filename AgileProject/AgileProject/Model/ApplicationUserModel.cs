using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgileProject.Entidades;

namespace AgileProject.Model
{
    public class AspNetUsersModel
    {
        public int CustomId { get; set; }
        
        public string? Id { get; set; }

        public string? Nombres { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string? FechaNacimientoStr { get; set; }

        public string? UserName { get; set; }
        public string? Clave { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }


        public int? Role { get; set; }

        
        public List<int>? Teams { get; set; }

        public string? TeamsListStr { get; set; }

        public IEnumerable<AspNetUserRoles> roleList { get; set; }
        public IEnumerable<UserCalendarType> teamsList { get; set; }

        //public string? Clave { get; set; }

        [NotMapped]
        public bool MantenerActivo { get; set; }

    }
}

