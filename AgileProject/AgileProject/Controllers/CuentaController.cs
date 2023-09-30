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
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Collections;
using AgileProject.Services.Contratos;
using AgileProject.Services.Implementaciones;
//using static Humanizer.In;

namespace AgileProject.Controllers
{
    public class CuentaController : Controller
    {
        ICalendarServices _CalendarServices;
        Seguridad.Seguridad Seguridad = new Seguridad.Seguridad();
        private readonly IConfiguration _configuration;
        //private readonly Contexto _contexto;

        //public CuentaController(Contexto contexto)
        //{
        //    _contexto = contexto;
        //}

        public CuentaController(IConfiguration configuration, ICalendarServices calendarServices)
        {
            _CalendarServices = calendarServices;
            _configuration = configuration;
        }

       

        public IActionResult Login()
        {
            ViewBag.IsSignedIn = false;
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                {
                    ViewBag.IsSignedIn = true;
                    return RedirectToAction("Index", "Home");
                }
                    
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AspNetUsersModel u)
        {
            ViewBag.IsSignedIn = false;
            AspNetUsers appUser = new AspNetUsers();

            try
            {
   
                var passEncrypt = Seguridad.Encriptar(u.Clave);

                //var passDesencrypt = Seguridad.DesEncriptar(u.Clave);
                appUser.UserName = u.UserName;
                appUser.PasswordHash = passEncrypt;
                appUser = await _CalendarServices.ValidarUsuario(appUser);
                //ViewBag.Error = appUser.Error;

                

                if (String.IsNullOrEmpty(ViewBag.Error))
                {
                    var myevent = appUser;

                    //roles
                    IEnumerable<AspNetUserRoles> _userRoleList = await _CalendarServices.GetAspNetUserRolesAsync(myevent.Id);


                    var strRoleList = string.Empty;
                    foreach (var item in _userRoleList)
                        strRoleList += item.RoleId + "|" + item.Name + "|" + item.NormalizedName + ",";
                    strRoleList = RemoveEnd(strRoleList, 1);

                    //teams
                    IEnumerable<UserCalendarType> _userTeamsList = await _CalendarServices.GetUserCalendarTypeAsync(u.UserName);


                    var strUserTeamsList = string.Empty;
                    foreach (var item in _userTeamsList)
                        strUserTeamsList += item.CalendarTypeId + "|" + item.Name + ",";
                    strUserTeamsList = RemoveEnd(strUserTeamsList, 1);


                    //add to claim
                    List<Claim> c = new List<Claim>()
                                {

                                    new Claim(ClaimTypes.Name, u.UserName),
                                    new Claim(ClaimTypes.NameIdentifier, myevent.Nombres),
                                    new Claim(ClaimTypes.Surname, myevent.ApellidoPaterno),
                                    new Claim(ClaimTypes.GivenName, myevent.ApellidoMaterno),
                                    new Claim(ClaimTypes.DateOfBirth, myevent.FechaNacimiento.ToShortDateString()),
                                    new Claim(ClaimTypes.Email, myevent.Email),
                                    new Claim(ClaimTypes.MobilePhone, "Generic"),
                                    new Claim(ClaimTypes.Sid, myevent.Id),
                                    new Claim(ClaimTypes.Role, "Generic"),
                                    new Claim("UserRoles", string.Join(",", strRoleList)),
                                    new Claim("UserTeams", string.Join(",", strUserTeamsList))
                                };
                    ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties p = new();

                    p.AllowRefresh = true;
                    p.IsPersistent = u.MantenerActivo;


                    if (!u.MantenerActivo)
                        p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60);
                    else
                        p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
                    //

                    //ViewBag.roleList = _userRoleList;
                    //ViewBag.userTeamsList = _userTeamsList;
                    //ViewBag.IsSignedIn = true;


                    return RedirectToAction("Index", "Home");
                }

                return View();

            }
            catch (System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        public String RemoveEnd(String str, int len)
        {
            if (str.Length < len)
            {
                return string.Empty;
            }

            return str[..^len];
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
