
namespace AgileProject.Entidades
{
    public class AspNetUserRoles //: AspNetRoles
    {
        public string? UserId { get; set; }
        //public string? RoleId { get; set; }
        //public string? UserName { get; set; }

        //public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string? UserName { get; set; }

        public int Id { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
    }
}
