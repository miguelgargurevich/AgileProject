using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AgileProject.Models;
using AgileProject.Entidades;
using AgileProject.Model;
using AgileProject.Services.Contratos;
using AutoMapper;
//using Microsoft.AspNetCore.Identity;
using AgileProject.Services.Implementaciones;

namespace AgileProject.Controllers;

public class KudoController : Controller
{
    private readonly IConfiguration _config;
    ICalendarServices _CalendarServices;
    IClaimsServices _ClaimsServices;
    private readonly ILogger<CalendarController> _logger;
    private IMapper _mapper;

    string? _urlApiSeguridad;
    string? _rutaApiSeguridad;
    //CalendarModel calendarModel = new CalendarModel();

    //public List<EventTypeModel>? EvenTypeList { get; set; }
   // public List<CalendarModel>? CalendarList { get; set; }


    public KudoController(
        IConfiguration config,
        ICalendarServices AgileProjectServices,
        IClaimsServices ClaimsServices,
        ILogger<CalendarController> logger,
        IMapper mapper)
    {
        _config = config;
        _CalendarServices = AgileProjectServices;
        _ClaimsServices = ClaimsServices;
        _logger = logger;
        _mapper = mapper;

        _urlApiSeguridad = _config["URL_API_SEGURIDAD"];
        _rutaApiSeguridad = _config["RUTA_API_SEGURIDAD"];
    }

    public async Task<IActionResult> IndexAsync()
    {
        //var _userModel = new AspNetUsersModel();
        //string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;

        ////usuario
        //IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(userName);
        //var _user = userList.ToList().FirstOrDefault();

        ////roles
        //IEnumerable<AspNetUserRoles> _userRoleList = await _CalendarServices.GetAspNetUserRolesAsync(_user.Id);
        //ViewBag.roleList = _userRoleList;

        //equipos
        //IEnumerable<UserCalendarType> _userTeamsList = await _CalendarServices.GetUserCalendarTypeAsync(userName);

        var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
        var _user = new AspNetUsersModel();
        _user = _ClaimsServices.getUserClaims(claims);

        List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
        _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

        List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
        _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

        if (_userTeamsList.Count == 0)
        {
            return RedirectToAction("Index", "User");
        }


        ViewBag.roleList = _userRoleList;
        ViewBag.userTeamsList = _userTeamsList;


        if (_userTeamsList.Count == 0)
        {
            return RedirectToAction("Index", "User");
        }


        ViewBag.Id = _user.Id;
        ViewBag.FechaNacimientoStr = String.Format("{0:dd/MM/yyyy}", _user.FechaNacimiento);
        ViewBag.Nombres = _user.Nombres;
        ViewBag.ApellidoPaterno = _user.ApellidoPaterno;
        ViewBag.ApellidoMaterno = _user.ApellidoMaterno;
        ViewBag.PhoneNumber = _user.PhoneNumber;
        ViewBag.UserName = _user.UserName;
        ViewBag.Email = _user.Email;
        ViewBag.IsSignedIn = true;

        return View();

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

