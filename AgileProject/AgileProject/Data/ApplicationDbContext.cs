using AgileProject.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgileProject.Data;

public class ApplicationDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("dbDocker"));
    }

    //public DbSet<UserRoles> UserRoles { get; set; }
    public DbSet<UserCalendarType> UserCalendarType { get; set; }
    public DbSet<EventType> EventType { get; set; }
    public DbSet<CalendarType> CalendarType { get; set; }
    public DbSet<Calendar> Calendar { get; set; }
    public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
    public DbSet<AspNetRoles> AspNetRoles { get; set; }
    public DbSet<AspNetUsers> AspNetUsers { get; set; }

}

