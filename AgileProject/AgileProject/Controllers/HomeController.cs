using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AgileProject.Models;
using AgileProject.Entidades;
using AgileProject.Model;
using AgileProject.Services.Contratos;
using AutoMapper;
//using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace AgileProject.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IConfiguration _config;
    ICalendarServices _CalendarServices;
    IClaimsServices _ClaimsServices;
    private readonly ILogger<CalendarController> _logger;
    private IMapper _mapper;
   

    public HomeController(
        IConfiguration config,
        ICalendarServices CalendarServices,
        IClaimsServices ClaimsServices,
        ILogger<CalendarController> logger,
        IMapper mapper)
    {
        _config = config;
        _CalendarServices = CalendarServices;
        _ClaimsServices = ClaimsServices;
        _logger = logger;
        _mapper = mapper;

    }

    public async Task<IActionResult> IndexAsync()
    {
        var userName = HttpContext.User.Identities.FirstOrDefault()?.Name;

        ViewBag.IsSignedIn = false;

        if (userName != null)
        {
            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            bool validClaims = _ClaimsServices.validClaims(claims);
            if (validClaims)
            {
                var _userLogin = new AspNetUsersModel();
                _userLogin = _ClaimsServices.getUserClaims(claims);

                List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
                _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

                List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
                _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

                ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);


                //if (ViewBag?.FechaNacimientoStr == "01/01/1900")
                //    return RedirectToAction("Index", "User");

                //if (_userRoleList.ToList().Count == 0 || _userTeamsList.ToList().Count == 0)
                //    return RedirectToAction("Index", "User");

                //else

                ViewBag.Id = _userLogin.Id;
                ViewBag.FechaNacimientoStr = String.Format("{0:dd/MM/yyyy}", _userLogin.FechaNacimiento);
                ViewBag.Nombres = _userLogin.Nombres;
                ViewBag.ApellidoPaterno = _userLogin.ApellidoPaterno;
                ViewBag.ApellidoMaterno = _userLogin.ApellidoMaterno;
                ViewBag.PhoneNumber = _userLogin.PhoneNumber;
                ViewBag.UserName = _userLogin.UserName;
                ViewBag.Email = _userLogin.Email;

                ViewBag.IsSignedIn = true;

            }
            else
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Cuenta");
            }


        }

        return View();


    }


    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Salir()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Cuenta");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

