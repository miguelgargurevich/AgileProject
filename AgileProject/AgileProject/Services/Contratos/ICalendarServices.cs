using AgileProject.Entidades;
using System;
using System.Collections.Generic;
//using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AgileProject.Services.Contratos
{
    public interface ICalendarServices
    {

        #region "Calendario"

       
        Task<IEnumerable<EventType>> GetEventTypesAsync();
        Task<Calendar> PostEventAddAsync(Calendar Calendar);
        Task<Calendar> PostEventUpdAsync(Calendar Calendar);
        Task<Calendar> PostEventDelAsync(Calendar Calendar);
        Task<IEnumerable<Calendar>> GetCalendarAsync(string idList);
        Task<CalendarType> PostCalendarTypeAddAsync(CalendarType CalendarType);
        Task<CalendarType> PostCalendarTypeUpdAsync(CalendarType CalendarType);
        Task<AspNetRoles> GetAspNetRolesAsync(int id);
        //Task<CalendarType> GetCalendarTypeAsync(string id);
        Task<AspNetRoles> PostAspNetRolesAddAsync(AspNetRoles aspNetRoles);
        Task<AspNetRoles> PostAspNetRolesUpdAsync(AspNetRoles aspNetRoles);
        #endregion

        #region "Usuario"

        Task<IEnumerable<AspNetUsers>> GetUsersAsync(string userName);
        Task<IEnumerable<AspNetRoles>> GetRolesAsync();
        Task<IEnumerable<CalendarType>> GetCalendarTypeAsync(int id);
        Task<AspNetUsers> PostUserUpdAsync(AspNetUsers userBE);
        Task<UserCalendarType> PostUserCalendarTypeAddAsync(UserCalendarType UserCalendarType);
        Task<UserCalendarType> PostUserCalendarTypeDelAsync(UserCalendarType UserCalendarType);
        Task<AspNetUserRoles> PostAspNetUserRolesAddAsync(AspNetUserRoles aspNetUserRoles);
        Task<IEnumerable<AspNetUserRoles>> GetAspNetUserRolesAsync(string userId);
        Task<IEnumerable<UserCalendarType>> GetUserCalendarTypeAsync(string userName);
        Task<AspNetUserRoles> PostAspNetUserRolesDelAsync(AspNetUserRoles aspNetUserRoles);
        Task<AspNetUsers> PostAspNetUserAddAsync(AspNetUsers AspNetUsers);
        Task<AspNetUsers> ValidarUsuario(AspNetUsers login);
        Task<AspNetUsers> ChangePassword(AspNetUsers user);
        #endregion



    }

}
