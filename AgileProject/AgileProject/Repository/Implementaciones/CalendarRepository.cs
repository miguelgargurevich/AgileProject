
using AgileProject.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AgileProject.Repository.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using AgileProject.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
//using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using AgileProject.Model;
using System.Collections;
//using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AgileProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using AutoMapper;
//using System.Globalization;

namespace AgileProject.Repository.Implementaciones
{

    public class CalendarRepository : ICalendarRepository
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<CalendarRepository> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CalendarRepository(IConfiguration configuration, ILogger<CalendarRepository> logger,
            IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _mapper = mapper;

        }

        #region "Calendario"

        public async Task<IEnumerable<EventType>> GetEventTypesAsync()
        {

            List<EventType> list = new List<EventType>();
            try
            {
                list = await _context.EventType.ToListAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }


            return list;

        }

        public async Task<Calendar> PostEventAddAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();

            try
            {
                _context.Calendar.Add(Calendar);
                _ = await _context.SaveChangesAsync();
                list.id = Calendar.id;

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

           

            return list;

        }  

        public async Task<CalendarType> PostCalendarTypeAddAsync(CalendarType CalendarType)
        {

            CalendarType list = new CalendarType();

            try
            {
                _context.CalendarType.Add(CalendarType);
                _ = await _context.SaveChangesAsync();
                list.Id = CalendarType.Id;

            }
            catch (Exception e)
            {
                CapturarError(e);
            }


            return list;


        }

        public async Task<CalendarType> PostCalendarTypeUpdAsync(CalendarType CalendarType)
        {
            CalendarType list = new CalendarType();

            try
            {
                _context.CalendarType.Update(CalendarType);
                _ = await _context.SaveChangesAsync();
                list.Id = CalendarType.Id;

            }
            catch (Exception e)
            {
                CapturarError(e);
            }


            return list;

        }

        public async Task<Calendar> PostEventUpdAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();

            try
            {
                _context.Calendar.Update(Calendar);
                _ = await _context.SaveChangesAsync();
                list.id = Calendar.id;
            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<Calendar> PostEventDelAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();

            try
            {
                var calendarToDelete = await _context.Calendar.FindAsync(Calendar.id);

                _ = _context.Calendar.Remove(calendarToDelete);
                _ = await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<IEnumerable<Calendar>> GetCalendarAsync(string idList)
        {
            string[] partes;
            if (idList.Contains(","))
                partes = idList.Split(',');
            else
                partes = new List<string> { idList }.ToArray();

            var ids = new List<int>();
            foreach (string parte in partes)
            {
                if (int.TryParse(parte, out int id))
                    ids.Add(id);
            }

            List<Calendar> list = new List<Calendar>();

            try
            {
                IQueryable<Calendar> queryResult = from cal in _context.Calendar
                                                   join eve in _context.EventType on cal.EventTypeId equals eve.Id into Events
                                                   from m in Events.DefaultIfEmpty()
                                                   where ids.Contains(cal.CalendarTypeId)
                                                   select new Calendar
                                                   {
                                                       id = cal.id,
                                                       title = cal.title,
                                                       start = cal.start,
                                                       end = cal.end,
                                                       allDay = cal.allDay,
                                                       color = m.Color,
                                                       EventTypeId = cal.EventTypeId,
                                                       type = m.Name,
                                                       CalendarTypeId = cal.CalendarTypeId,
                                                       CalendarTypeName = cal.CalendarTypeName,
                                                       description = cal.description,
                                                       UserName = cal.UserName
                                                   };

                list = await queryResult.ToListAsync(); 

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;
        }

    
        #endregion

        #region "Usuarios"

        public async Task<IEnumerable<AspNetUsers>> GetUsersAsync(string userName)
        {

            List<AspNetUsers> list = new List<AspNetUsers>();
            try
            {
                if (!String.IsNullOrEmpty(userName))
                    list = await _context.AspNetUsers.Where(x => x.UserName.Equals(userName)).ToListAsync();
                else
                    list = await _context.AspNetUsers.ToListAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<IEnumerable<AspNetRoles>> GetRolesAsync()
        {

            List<AspNetRoles> list = new List<AspNetRoles>();
            try
            {
                list = await _context.AspNetRoles.Where(x => x.Name != "ADM").ToListAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }
          
        public async Task<IEnumerable<CalendarType>> GetCalendarTypeAsync()
        {
            List<CalendarType> list = new List<CalendarType>();
            try
            {
                list = await _context.CalendarType.ToListAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<AspNetUsers> PostUserUpdAsync(AspNetUsers userBE)
        {
            AspNetUsers list = new AspNetUsers();

            try
            {
                _ = _context.AspNetUsers.Update(userBE);
                _ = await _context.SaveChangesAsync();
                list.Id = userBE.Id;

            }
            catch (Exception e)
            {
                CapturarError(e);
            }
           

            return list;

        }
       
        public async Task<UserCalendarType> PostUserCalendarTypeAddAsync(UserCalendarType UserCalendarType)
        {

            UserCalendarType list = new UserCalendarType();

            try
            {
                _ = _context.UserCalendarType.AddAsync(UserCalendarType);
                _ = await _context.SaveChangesAsync();
                list.Id = UserCalendarType.Id;

            }
            catch (Exception e)
            {
                CapturarError(e);
            }


            return list;

        }

        public async Task<UserCalendarType> PostUserCalendarTypeDelAsync(UserCalendarType UserCalendarType)
        {
            UserCalendarType list = new UserCalendarType();

            try
            {
                var del = await _context.Calendar.FindAsync(UserCalendarType.Id);

                _ = _context.Calendar.Remove(del);
                _ = await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<AspNetUserRoles> PostAspNetUserRolesDelAsync(AspNetUserRoles aspNetUserRoles)
        {
            AspNetUserRoles list = new AspNetUserRoles();

            try
            {
                var del = await _context.Calendar.FindAsync(aspNetUserRoles.Id);

                _ = _context.Calendar.Remove(del);
                _ = await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<AspNetUserRoles> PostAspNetUserRolesAddAsync(AspNetUserRoles aspNetUserRoles)
        {
            AspNetUserRoles list = new AspNetUserRoles();

            try
            {
                _ = _context.AspNetUserRoles.AddAsync(aspNetUserRoles);
                _ = await _context.SaveChangesAsync();
                list.Id = aspNetUserRoles.Id;

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<IEnumerable<AspNetUserRoles>> GetAspNetUserRolesAsync(string userId)
        {

            List<AspNetUserRoles> list = new List<AspNetUserRoles>();

            try
            {
                IQueryable<AspNetUserRoles> queryResult = from userroles in _context.AspNetUserRoles
                                                          join users in _context.AspNetUsers on userroles.UserId equals users.Id into Users
                                                          from _Users in Users.DefaultIfEmpty()
                                                          join roles in _context.AspNetRoles on userroles.RoleId equals roles.Id into Roles
                                                          from _Roles in Roles.DefaultIfEmpty()
                                                          where userroles.UserId.Contains(userId)
                                                           select new AspNetUserRoles
                                                           {
                                                               UserId = userroles.UserId,
                                                               UserName = _Users.UserName,
                                                               RoleId = userroles.RoleId,
                                                               Name = _Roles.Name,
                                                               NormalizedName = _Roles.NormalizedName,
                                                              
                                                           };

                list = await queryResult.ToListAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<IEnumerable<UserCalendarType>> GetUserCalendarTypeAsync(string userName)
        {
            //System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es");
            List<UserCalendarType> list = new List<UserCalendarType>();

            try
            {
                IQueryable<UserCalendarType> queryResult = from userCalendarType in _context.UserCalendarType
                                                           join calendarType in _context.CalendarType on userCalendarType.CalendarTypeId equals calendarType.Id into CalendarType
                                                           from _CalendarType in CalendarType.DefaultIfEmpty()
                                                          where userCalendarType.UserName.Contains(userName)
                                                          select new UserCalendarType
                                                          {
                                                              UserName = userCalendarType.UserName,
                                                              CalendarTypeId = userCalendarType.CalendarTypeId,
                                                              Name = _CalendarType.Name,
                                                              GroupType = _CalendarType.GroupType,

                                                          };

                list = await queryResult.ToListAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;
        }


        public async Task<AspNetUsers> PostAspNetUserAddAsync(AspNetUsers AspNetUsers)
        {

            Guid obj = Guid.NewGuid();
            var genPass = "AQAAAAEAACcQAAAAEEwFaE3V8mEGzrhV36TThPLx1GI7E7pc4Neq04qwNBk/qWJr1Da7OYLt3F5H1y4cXg=="; //Password1!

            AspNetUsers.Id = obj.ToString();
            AspNetUsers.PasswordHash = genPass;

            try
            {
                _ = _context.AspNetUsers.AddAsync(AspNetUsers);
                _ = await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return AspNetUsers;

        }


        public async Task<AspNetRoles> GetAspNetRolesAsync(int id)
        {

            AspNetRoles myevent = new AspNetRoles();

            try
            {
                myevent = await _context.AspNetRoles.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return myevent;
        }

        public async Task<IEnumerable<CalendarType>> GetCalendarTypeAsync(int id)
        {

            //CalendarType myevent = new CalendarType();
            List<CalendarType> list = new List<CalendarType>();

            try
            {
                if (id == 0)
                    list = await _context.CalendarType.ToListAsync();
                else
                    list = await _context.CalendarType.Where(x => x.Id.Equals(id)).ToListAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;
        }

        public async Task<AspNetRoles> PostAspNetRolesAddAsync(AspNetRoles aspNetRoles)
        {
            AspNetRoles list = new AspNetRoles();

            try
            {
                _ = _context.AspNetRoles.AddAsync(aspNetRoles);
                _ = await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;

        }

        public async Task<AspNetRoles> PostAspNetRolesUpdAsync(AspNetRoles aspNetRoles)
        {
            AspNetRoles list = new AspNetRoles();

            try
            {
                _ = _context.AspNetRoles.Update(aspNetRoles);
                _ = await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;
        }

        public async Task<AspNetUsers> ValidarUsuario(AspNetUsers login)
        {
            AspNetUsers userModel = new AspNetUsers();
            try
            {
                AspNetUsers user = await _context.AspNetUsers.Where(x => x.UserName.Equals(login.UserName) && x.PasswordHash.Equals(login.PasswordHash)).FirstOrDefaultAsync();
                userModel = _mapper.Map<AspNetUsers>(user); //map ok just missing register in mappingProfile class
            }
            catch (Exception e)
            {
                CapturarError(e);
            }
           

            return userModel;

        }

        public async Task<AspNetUsers> ChangePassword(AspNetUsers user)
        {
            try
            {
                // 1. Recuperar el usuario de la base de datos.
                //var userdb = await _context.AspNetUsers.SingleOrDefaultAsync(u => u.UserName == user.UserName);

                // 3. Actualizar la contraseña con la nueva contraseña proporcionada por el usuario.
                //userdb.PasswordHash = user.PasswordHash;

                // 4. Guardar los cambios en la base de datos.
                _ = _context.Update(user);
                _ = await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            

            return user;
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
