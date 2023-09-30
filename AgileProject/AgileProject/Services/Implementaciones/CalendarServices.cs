using AgileProject.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Globalization;
using AgileProject.Services.Contratos;
using AgileProject.Repository.Contratos;
using System.Collections;

namespace AgileProject.Services.Implementaciones
{
    public class CalendarServices : ICalendarServices
    {

        private readonly IConfiguration _config;
        private readonly ICalendarRepository _AgileProjectRepository;
        private readonly ILogger<CalendarServices> _logger;
        IEmailServices _emailServices;

        private string claveAcceso;

        public CalendarServices(IConfiguration config, ICalendarRepository AgileProjectRepository,
            ILogger<CalendarServices> logger, IEmailServices emailServices)
        {
            _config = config;
            _AgileProjectRepository = AgileProjectRepository;
            _logger = logger;
            _emailServices = emailServices;
            claveAcceso = _config.GetValue<string>("Claves:CLAVE_GENERICA");
        }

        #region "Calendario"

       

        public async Task<IEnumerable<EventType>> GetEventTypesAsync()
        {
            IEnumerable<EventType> list = new List<EventType>();
            try
            {
                list = await _AgileProjectRepository.GetEventTypesAsync();
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "AgileProjectService", "GetEventTypesAsync");
            }

            return list;

        }

        public async Task<Calendar> PostEventAddAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();
            try
            {
                list = await _AgileProjectRepository.PostEventAddAsync(Calendar);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "AgileProjectService", "PostEventAddAsync");
            }

            return list;

        }

        public async Task<Calendar> PostEventUpdAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();
            try
            {
                list = await _AgileProjectRepository.PostEventUpdAsync(Calendar);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "AgileProjectService", "PostEventUpdAsync");
            }

            return list;

        }

        public async Task<Calendar> PostEventDelAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();
            try
            {
                list = await _AgileProjectRepository.PostEventDelAsync(Calendar);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "AgileProjectService", "PostEventDelAsync");
            }

            return list;

        }
        
        public async Task<IEnumerable<Calendar>> GetCalendarAsync(string idList)
        {
            IEnumerable<Calendar> list = new List<Calendar>();
            try
            {
                list = await _AgileProjectRepository.GetCalendarAsync(idList);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "AgileProjectService", "GetCalendarAsync");
            }

            return list;

        }

        public async Task<CalendarType> PostCalendarTypeAddAsync(CalendarType CalendarType)
        {
            CalendarType list = new CalendarType();
            try
            {
                list = await _AgileProjectRepository.PostCalendarTypeAddAsync(CalendarType);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "AgileProjectService", "PostCalendarTypeAddAsync");
            }

            return list;
        }
        public async Task<CalendarType> PostCalendarTypeUpdAsync(CalendarType CalendarType)
        {
            CalendarType list = new CalendarType();
            try
            {
                list = await _AgileProjectRepository.PostCalendarTypeUpdAsync(CalendarType);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "AgileProjectService", "PostCalendarTypeUpdAsync");
            }

            return list;
        }

       
        public async Task<AspNetRoles> GetAspNetRolesAsync(int id)
        {
            AspNetRoles list = new AspNetRoles();
            try
            {
                list = await _AgileProjectRepository.GetAspNetRolesAsync(id);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "GetAspNetRolesAsync", "GetAspNetRolesAsync");
            }

            return list;
        }


       
        public async Task<IEnumerable<CalendarType>> GetCalendarTypeAsync(int id)
        {
            IEnumerable<CalendarType> list = new List<CalendarType>();
            try
            {
                list = await _AgileProjectRepository.GetCalendarTypeAsync(id);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "GetCalendarTypeAsync", "GetCalendarTypeAsync");
            }

            return list;
        }


        
        public async Task<AspNetRoles> PostAspNetRolesAddAsync(AspNetRoles aspNetRoles)
        {
            AspNetRoles list = new AspNetRoles();
            try
            {
                list = await _AgileProjectRepository.PostAspNetRolesAddAsync(aspNetRoles);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "PostAspNetRolesAddAsync", "PostAspNetRolesAddAsync");
            }

            return list;
        }
        
            public async Task<AspNetRoles> PostAspNetRolesUpdAsync(AspNetRoles aspNetRoles)
        {
            AspNetRoles list = new AspNetRoles();
            try
            {
                list = await _AgileProjectRepository.PostAspNetRolesUpdAsync(aspNetRoles);
            }
            catch (Exception ex)
            {
                //list.code = "-1";
                //list.message = "Error: " + ex.Message.ToString();

                CapturarError(ex, "PostAspNetRolesUpdAsync", "PostAspNetRolesUpdAsync");
            }

            return list;
        }

        #endregion

        #region "Usuarios"

        public async Task<IEnumerable<AspNetUsers>> GetUsersAsync(string userName)
        {

            IEnumerable<AspNetUsers> list = new List<AspNetUsers>();
            try
            {
                list = await _AgileProjectRepository.GetUsersAsync(userName);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "GetUsersAsync");
            }

            return list;

        }

        
        public async Task<IEnumerable<AspNetRoles>> GetRolesAsync()
        {

            IEnumerable<AspNetRoles> list = new List<AspNetRoles>();
            try
            {
                list = await _AgileProjectRepository.GetRolesAsync();
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "GetRolesAsync");
            }

            return list;

        }

        
        //public async Task<IEnumerable<CalendarType>> GetCalendarTypeAsync()
        //{

        //    IEnumerable<CalendarType> list = new List<CalendarType>();
        //    try
        //    {
        //        list = await _AgileProjectRepository.GetCalendarTypeAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        CapturarError(ex, "AgileProjectService", "GetCalendarTypeAsync");
        //    }

        //    return list;

        //}


        public async Task<AspNetUsers> PostUserUpdAsync(AspNetUsers userBE)
        {

            AspNetUsers list = new AspNetUsers();
            try
            {
                list = await _AgileProjectRepository.PostUserUpdAsync(userBE);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "PostUserUpdAsync");
            }

            return list;

        }


        public async Task<UserCalendarType> PostUserCalendarTypeAddAsync(UserCalendarType UserCalendarType)
        {

            UserCalendarType list = new UserCalendarType();
            try
            {
                list = await _AgileProjectRepository.PostUserCalendarTypeAddAsync(UserCalendarType);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "PostUserCalendarTypeAddAsync");
            }

            return list;

        }

        
        public async Task<UserCalendarType> PostUserCalendarTypeDelAsync(UserCalendarType UserCalendarType)
        {

            UserCalendarType list = new UserCalendarType();
            try
            {
                list = await _AgileProjectRepository.PostUserCalendarTypeDelAsync(UserCalendarType);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "PostUserCalendarTypeDelAsync");
            }

            return list;

        }

        public async Task<AspNetUserRoles> PostAspNetUserRolesAddAsync(AspNetUserRoles aspNetUserRoles)
        {

            AspNetUserRoles list = new AspNetUserRoles();
            try
            {
                list = await _AgileProjectRepository.PostAspNetUserRolesAddAsync(aspNetUserRoles);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "PostAspNetUserRolesAddAsync");
            }

            return list;

        }

        public async Task<IEnumerable<AspNetUserRoles>> GetAspNetUserRolesAsync(string userId)
        {
            IEnumerable<AspNetUserRoles> list = new List<AspNetUserRoles>();
            try
            {
                list = await _AgileProjectRepository.GetAspNetUserRolesAsync(userId);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "GetAspNetUserRolesAsync");
            }

            return list;

        }

        public async Task<IEnumerable<UserCalendarType>> GetUserCalendarTypeAsync(string userName)
        {

            IEnumerable<UserCalendarType> list = new List<UserCalendarType>();
            try
            {
                list = await _AgileProjectRepository.GetUserCalendarTypeAsync(userName);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "GetUserCalendarTypeAsync");
            }

            return list;

        }

        public async Task<AspNetUserRoles> PostAspNetUserRolesDelAsync(AspNetUserRoles aspNetUserRoles)
        {

            AspNetUserRoles list = new AspNetUserRoles();
            try
            {
                list = await _AgileProjectRepository.PostAspNetUserRolesDelAsync(aspNetUserRoles);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "PostAspNetUserRolesDelAsync");
            }

            return list;

        }

        public async Task<AspNetUsers> PostAspNetUserAddAsync(AspNetUsers AspNetUsers)
        {
            AspNetUsers list = new AspNetUsers();
            try
            {
                list = await _AgileProjectRepository.PostAspNetUserAddAsync(AspNetUsers);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "AgileProjectService", "PostAspNetUserAddAsync");
            }

            return list;

        }

        public async Task<AspNetUsers> ValidarUsuario(AspNetUsers login)
        {
            AspNetUsers list = new AspNetUsers();
            try
            {
                list = await _AgileProjectRepository.ValidarUsuario(login);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "ValidarUsuario", "ValidarUsuario");
            }

            return list;

        }

        public async Task<AspNetUsers> ChangePassword(AspNetUsers user)
        {
            AspNetUsers list = new AspNetUsers();
            try
            {
                list = await _AgileProjectRepository.ChangePassword(user);
            }
            catch (Exception ex)
            {

                CapturarError(ex, "ChangePassword", "ChangePassword");
            }

            return list;

        }

        #endregion


        #region "Control de errores"

        public void CapturarError(Exception error, string controlador = "", string accion = "")
        {
            var msg = error.Message;
            if (error.InnerException != null)
            {
                msg = msg + "/;/" + error.InnerException.Message;
                if (error.InnerException.InnerException != null)
                {
                    msg = msg + "/;/" + error.InnerException.InnerException.Message;
                    if (error.InnerException.InnerException.InnerException != null)
                        msg = msg + "/;/" + error.InnerException.InnerException.InnerException.Message;
                }
            }

            var fechahora = DateTime.Now.ToString();
            var comentario = $@"***ERROR: [{fechahora}] [{controlador}/{accion}] - MensajeError: {msg}";
            string errorFormat = string.Format("{0} | {1}", comentario, error.StackTrace);
            _logger.LogError(errorFormat);

        }

        #endregion

    }
}
