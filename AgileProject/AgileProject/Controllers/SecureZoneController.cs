using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using System.Data.SqlClient;
using AgileProject.Model;
using AgileProject.Seguridad;
using Microsoft.Extensions.Configuration;
using AgileProject.Entidades;
using AgileProject.Services.Contratos;
using AutoMapper;
using AgileProject.Services.Implementaciones;

namespace AgileProject.Controllers
{
    public class SecureZone : Controller
    {
        Seguridad.Seguridad Seguridad = new Seguridad.Seguridad();
        private readonly IConfiguration _configuration;
        ICalendarServices _CalendarServices;
        IClaimsServices _ClaimsServices;
        private readonly ILogger<CalendarController> _logger;
        private IMapper _mapper;
        //private readonly Contexto _contexto;

        //public CuentaController(Contexto contexto)
        //{
        //    _contexto = contexto;
        //}
        string? connectionString = String.Empty;
        public SecureZone(IConfiguration configuration,
            ICalendarServices AgileProjectServices,
            IClaimsServices ClaimsServices,
            ILogger<CalendarController> logger,
            IMapper mapper)
        {
            _configuration = configuration;
            _CalendarServices = AgileProjectServices;
            _ClaimsServices = ClaimsServices;
            _logger = logger;
            _mapper = mapper;

            connectionString = _configuration.GetConnectionString("dbDesa");
        }

       

        public async Task<IActionResult> IndexAsync()
        {
            
            var userName = HttpContext.User.Identity.Name;
            var customClaim = HttpContext.User.FindFirst("MyCustomClaim");
            ViewBag.IsSignedIn = false;

            if (!String.IsNullOrEmpty(userName))
            {
                ////usuario
                //IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(userName);
                //var _userLogin = userList.ToList().FirstOrDefault();

                ////roles
                //IEnumerable<AspNetUserRoles> _userRoleList = await _CalendarServices.GetAspNetUserRolesAsync(_userLogin.Id);

                var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
                var _userLogin = new AspNetUsersModel();
                _userLogin = _ClaimsServices.getUserClaims(claims);

                List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
                _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

                List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
                _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);


                ViewBag.roleList = _userRoleList;

                ViewBag.Id = _userLogin.Id;
                ViewBag.FechaNacimientoStr = String.Format("{0:dd/MM/yyyy}", _userLogin.FechaNacimiento);
                ViewBag.Nombres = _userLogin.Nombres;
                ViewBag.ApellidoPaterno = _userLogin.ApellidoPaterno;
                ViewBag.ApellidoMaterno = _userLogin.ApellidoMaterno;
                ViewBag.PhoneNumber = _userLogin.PhoneNumber;
                ViewBag.UserName = userName;
                ViewBag.Email = userName;

                ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);

                ViewBag.IsSignedIn = true;

                
            }
          
            return View();
        }

        [Route("SecureZone/ChangePassword")]
        [HttpGet]
        public async Task<IActionResult> ChangePasswordAsync()
        {
            //AspNetUsersModel usuario = new AspNetUsersModel();

            var userName = HttpContext.User.Identity.Name;
            var customClaim = HttpContext.User.FindFirst("MyCustomClaim");
            ViewBag.IsSignedIn = false;

            if (!String.IsNullOrEmpty(userName))
            {
                ////usuario
                //IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(userName);
                //var _userLogin = userList.ToList().FirstOrDefault();

                ////roles
                //IEnumerable<AspNetUserRoles> _userRoleList = await _CalendarServices.GetAspNetUserRolesAsync(_userLogin.Id);


                var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
                var _userLogin = new AspNetUsersModel();
                _userLogin = _ClaimsServices.getUserClaims(claims);

                List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
                _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

                List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
                _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);


                ViewBag.roleList = _userRoleList;

                ViewBag.Id = _userLogin.Id;
                ViewBag.FechaNacimientoStr = String.Format("{0:dd/MM/yyyy}", _userLogin.FechaNacimiento);
                ViewBag.Nombres = _userLogin.Nombres;
                ViewBag.ApellidoPaterno = _userLogin.ApellidoPaterno;
                ViewBag.ApellidoMaterno = _userLogin.ApellidoMaterno;
                ViewBag.PhoneNumber = _userLogin.PhoneNumber;
                ViewBag.UserName = userName;
                ViewBag.Email = userName;

                ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);

                ViewBag.IsSignedIn = true;

                //usuario.Id_Usuario = _userLogin.Id;
                //usuario.UserName = userName;
                //usuario.Clave = string.Empty;
            }


            return View();
        }

        [Route("SecureZone/SavePassword")]
        [HttpPost]
        public async Task<IActionResult> SavePasswordAsync(AspNetUsersModel usuario)
        {
            var clave = usuario.Clave; //.Clave; 
            var userName = HttpContext.User.Identity.Name;
            var customClaim = HttpContext.User.FindFirst("MyCustomClaim");

            ViewBag.IsSignedIn = false;
            ViewBag.IsOk = false;

            ////usuario
            //IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(userName);
            //var _userLogin = userList.ToList().FirstOrDefault();

            ////roles
            //IEnumerable<AspNetUserRoles> _userRoleList = await _CalendarServices.GetAspNetUserRolesAsync(_userLogin.Id);


            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _userLogin = new AspNetUsersModel();
            _userLogin = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

            ViewBag.roleList = _userRoleList;

            ViewBag.Id = _userLogin.Id;
            ViewBag.FechaNacimientoStr = String.Format("{0:dd/MM/yyyy}", _userLogin.FechaNacimiento);
            ViewBag.Nombres = _userLogin.Nombres;
            ViewBag.ApellidoPaterno = _userLogin.ApellidoPaterno;
            ViewBag.ApellidoMaterno = _userLogin.ApellidoMaterno;
            ViewBag.PhoneNumber = _userLogin.PhoneNumber;
            ViewBag.UserName = userName;
            ViewBag.Email = userName;
            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);

            ViewBag.IsSignedIn = true;

            try
            {

                var passEncrypt = Seguridad.Encriptar(clave);

                //var passDesencrypt = Seguridad.DesEncriptar(u.Clave);
                var appUser = new AspNetUsers();
                IEnumerable<AspNetUsers> listUser = await _CalendarServices.GetUsersAsync(userName);
                appUser = listUser.FirstOrDefault();

                appUser.PasswordHash = passEncrypt;

                var rpta = _CalendarServices.ChangePassword(appUser);
                ViewBag.IsOk = true;

            }
            catch (System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }

            return View("ChangePassword");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
