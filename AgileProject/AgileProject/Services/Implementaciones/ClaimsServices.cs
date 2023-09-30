using AgileProject.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using AgileProject.Services.Contratos;
using AgileProject.Repository.Contratos;
using System.Collections;
using AgileProject.Model;

namespace AgileProject.Services.Implementaciones
{
    public class ClaimsServices : IClaimsServices
    {

        private readonly IConfiguration _config;
        private readonly ICalendarRepository _AgileProjectRepository;
        private readonly ILogger<CalendarServices> _logger;
        IEmailServices _emailServices;

        private string claveAcceso;

        public ClaimsServices(IConfiguration config, ICalendarRepository AgileProjectRepository,
            ILogger<CalendarServices> logger, IEmailServices emailServices)
        {
            _config = config;
            _AgileProjectRepository = AgileProjectRepository;
            _logger = logger;
            _emailServices = emailServices;
            claveAcceso = _config.GetValue<string>("Claves:CLAVE_GENERICA");
        }

        public bool validClaims(List<System.Security.Claims.Claim> claims)
        {
            return claims.Count>=9;

        }

        public bool isAdmin(List<System.Security.Claims.Claim> claims)
        {
            return claims[9].Value.Contains("ADM");

        }

        public AspNetUsersModel getUserClaims(List<System.Security.Claims.Claim> claims)
        {
            var obj = new AspNetUsersModel();
            obj.UserName = claims[0].Value;
            obj.Nombres = claims[1].Value;
            obj.ApellidoPaterno = claims[2].Value;
            obj.ApellidoMaterno = claims[3].Value;
            obj.FechaNacimientoStr = String.Format("{0:dd/MM/yyyy}", claims[4].Value);
            obj.FechaNacimiento = DateTime.Parse(claims[4].Value);
            obj.Email = claims[5].Value;
            obj.PhoneNumber = claims[6].Value;
            obj.Id = claims[7].Value;

            return obj;


        }


        public List<AspNetUserRoles> getUserRoleClaims(List<System.Security.Claims.Claim> claims)
        {
            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();

            var userRolesClaims = claims[9].Value;

            if (userRolesClaims.Contains(","))
            {
                var _obj = userRolesClaims.Split(",");
                foreach (var item in _obj)
                {
                    AspNetUserRoles obj = new AspNetUserRoles();
                    obj.RoleId =  Convert.ToInt32(item.Split("|").GetValue(0)?.ToString());
                    obj.Name = item.Split("|").GetValue(1)?.ToString();
                    obj.NormalizedName = item.Split("|").GetValue(2)?.ToString();
                    _userRoleList.Add(obj);
                }
            }
            else
            {
                AspNetUserRoles obj = new AspNetUserRoles();
                obj.RoleId = Convert.ToInt32(userRolesClaims.Split("|").GetValue(0)?.ToString());
                obj.Name = userRolesClaims.Split("|").GetValue(1)?.ToString();
                obj.NormalizedName = userRolesClaims.Split("|").GetValue(2)?.ToString();
                _userRoleList.Add(obj);
            }

            return _userRoleList;


        }


        public List<UserCalendarType> getUserTeamsClaims(List<System.Security.Claims.Claim> claims)
        {
            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            var userTeamsClaims = claims[10].Value;
            if (!String.IsNullOrEmpty(userTeamsClaims))
            if (userTeamsClaims.Contains(","))
            {
                var _obj = userTeamsClaims.Split(",");
                foreach (var item in _obj)
                {
                    UserCalendarType obj = new UserCalendarType();
                    obj.CalendarTypeId = Convert.ToInt32(item.Split("|").GetValue(0)?.ToString());
                    obj.Name = item.Split("|").GetValue(1)?.ToString();

                    _userTeamsList.Add(obj);
                }
            }
            else
            {
                UserCalendarType obj = new UserCalendarType();
                obj.CalendarTypeId = Convert.ToInt32(userTeamsClaims.Split("|").GetValue(0)?.ToString());
                obj.Name = userTeamsClaims.Split("|").GetValue(1)?.ToString();

                _userTeamsList.Add(obj);
            }
            return _userTeamsList;


        }




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
