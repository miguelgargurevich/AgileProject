using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AgileProject.Models;
using AgileProject.Entidades;
using AgileProject.Model;
using AgileProject.Services.Contratos;
using AutoMapper;
//using Microsoft.AspNetCore.Identity;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using AgileProject.Seguridad;
using AgileProject.Services.Implementaciones;
using System.Security.Claims;

namespace AgileProject.Controllers;

public class UserController : Controller
{
    private readonly IConfiguration _config;
    ICalendarServices _CalendarServices;
    IClaimsServices _ClaimsServices;
    private readonly ILogger<CalendarController> _logger;
    private IMapper _mapper;
    Seguridad.Seguridad Seguridad = new();

    string? _urlApiSeguridad;
    string? _rutaApiSeguridad;


    //private readonly UserManager<AspNetUsers> _userManager;


    public UserController(
        IConfiguration config,
        ICalendarServices AgileProjectServices,
        IClaimsServices ClaimsServices,
        ILogger<CalendarController> logger,
        IMapper mapper
        //UserManager<AspNetUsers> userManager
        )
    {
        _config = config;
        _CalendarServices = AgileProjectServices;
        _ClaimsServices = ClaimsServices;
        _logger = logger;
        _mapper = mapper;
        //_userManager = userManager;

        _urlApiSeguridad = _config["URL_API_SEGURIDAD"];
        _rutaApiSeguridad = _config["RUTA_API_SEGURIDAD"];
    }

    public async Task<IActionResult> IndexAsync()
    {
        ViewBag.IsSignedIn = false;
        var _userModel = new AspNetUsersModel();
        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {

            //IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(userName);
            //AspNetUsers _user = userList.ToList().FirstOrDefault();
            ////_userModel = _mapper.Map<List<AspNetUsersModel>>(user.ToList()).FirstOrDefault();
            ///

            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);


            var fechaNacimientoSrt = String.Format("{0:yyyy-MM-dd}", _user.FechaNacimiento);
            if (fechaNacimientoSrt == "0001-01-01")
                fechaNacimientoSrt = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
            _userModel.CustomId = 1;
            _userModel.Id = _user.Id;
            _userModel.FechaNacimientoStr = fechaNacimientoSrt;
            _userModel.Nombres = _user.Nombres;
            _userModel.ApellidoPaterno = _user.ApellidoPaterno;
            _userModel.ApellidoMaterno = _user.ApellidoMaterno;
            _userModel.PhoneNumber = _user.PhoneNumber;

            //IEnumerable<AspNetUserRoles> _userRoleList = await _CalendarServices.GetAspNetUserRolesAsync(_user.Id);
            _userModel.roleList = _userRoleList;
            _userModel.Role = _userRoleList.FirstOrDefault()?.RoleId;

            //IEnumerable<UserCalendarType> _userTeamsList = await _CalendarServices.GetUserCalendarTypeAsync(userName);
            _userModel.teamsList = _userTeamsList;


            IEnumerable<AspNetRoles> _roleList = await _CalendarServices.GetRolesAsync();
            ViewBag.roleList = _roleList;

            //IEnumerable<CalendarType> _teamsList = await _CalendarServices.GetCalendarTypeAsync();

            ViewBag.UserName = userName;
            ViewBag.userTeamsList = _userTeamsList;
            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
            ViewBag.IsSignedIn = true;
        }


        return View(_userModel);
    }

    [Route("User/SaveProfile")]
    [HttpPost]
    public async Task<IActionResult> SaveProfileAsync(AspNetUsersModel model)
    {

        //string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;

        //var _userModelDB = _mapper.Map<List<AspNetUsersModel>>(user.ToList());

        model.FechaNacimientoStr = String.Format("{0:yyyy-MM-dd}", model.FechaNacimiento);

        AspNetUsers _user = new AspNetUsers();
        _user.Id = model.Id;
        _user.Nombres = model.Nombres?.ToString();
        _user.ApellidoPaterno = model.ApellidoPaterno?.ToString();
        _user.ApellidoMaterno = model.ApellidoMaterno?.ToString();
        _user.UserName = model.UserName;
        _user.FechaNacimiento = model.FechaNacimiento;
        _user.PhoneNumber = model.PhoneNumber;
        AspNetUsers userUpdate = await _CalendarServices.PostUserUpdAsync(_user);

        string? userNameLogin = HttpContext.User.Identities.FirstOrDefault()?.Name;
        IEnumerable<AspNetUsers> userListLogin = await _CalendarServices.GetUsersAsync(userNameLogin);
        var _userLogin = userListLogin.ToList().FirstOrDefault();
        IEnumerable<AspNetUserRoles> _userRoleListLogin = await _CalendarServices.GetAspNetUserRolesAsync(_userLogin.Id);
        ViewBag.IsAdmin = _userRoleListLogin.ToList().Where(x => x.Name == "ADM").Count() > 0;
        ViewBag.IsSignedIn = true;
        ViewBag.IsOk = true;

        return View("EditUser", model);
    }

    [Route("User/SaveOrganization")]
    [HttpPost]
    public async Task<IActionResult> SaveOrganizationAsync(AspNetUsersModel model)
    {
        ViewBag.IsSignedIn = false;
        var _userModel = new AspNetUsersModel();


        AspNetUserRoles _role = new AspNetUserRoles();
        _role.UserId = model.Id;
        _role.RoleId = model.Role;

        AspNetUserRoles roleDel = await _CalendarServices.PostAspNetUserRolesDelAsync(_role);
        AspNetUserRoles roleInsert = await _CalendarServices.PostAspNetUserRolesAddAsync(_role);

        UserCalendarType _team = new UserCalendarType();
        _team.UserName = model.UserName;

        UserCalendarType teamDelete = await _CalendarServices.PostUserCalendarTypeDelAsync(_team);
        if (model.Teams != null)
        {
            foreach (var item in model.Teams)
            {
                _team.CalendarTypeId = item;
                UserCalendarType teamInsert = await _CalendarServices.PostUserCalendarTypeAddAsync(_team);
            }
        }


        IEnumerable<AspNetRoles> _roleList = await _CalendarServices.GetRolesAsync();
        ViewBag.roleList = _roleList;

        IEnumerable<CalendarType> _teamsList = await _CalendarServices.GetCalendarTypeAsync(0);
        var teamsList = _teamsList.ToList();
        foreach (var item in teamsList)
        {
            if (model.Teams != null)
            {
                if (model.Teams.Count > 0)
                {
                    var list = model.Teams.Where(d => d == item.Id);
                    if (list.Count() > 0)
                        item.IsChecked = true;
                }
            }


        }
        ViewBag.teamsList = _teamsList;

        string? userNameLogin = HttpContext.User.Identities.FirstOrDefault()?.Name;
        IEnumerable<AspNetUsers> userListLogin = await _CalendarServices.GetUsersAsync(userNameLogin);
        var _userLogin = userListLogin.ToList().FirstOrDefault();
        IEnumerable<AspNetUserRoles> _userRoleListLogin = await _CalendarServices.GetAspNetUserRolesAsync(_userLogin.Id);
        ViewBag.IsAdmin = _userRoleListLogin.ToList().Where(x => x.Name == "ADM").Count() > 0;
        ViewBag.IsSignedIn = true;
        ViewBag.IsOk = true;

        return View("EditOrganization", model);
    }

    [Route("User/Save")]
    [HttpPost]
    public async Task<IActionResult> SaveAsync(AspNetUsersModel model)
    {
        ViewBag.IsSignedIn = false;
        model.FechaNacimientoStr = String.Format("{0:dd/MM/yyyy}", model.FechaNacimiento);

        AspNetUsers _user = new AspNetUsers();
        _user.Id = model.Id;
        _user.Nombres = model.Nombres;
        _user.ApellidoPaterno = model.ApellidoPaterno;
        _user.ApellidoMaterno = model.ApellidoMaterno;
        _user.UserName = model.UserName;
        _user.FechaNacimiento = model.FechaNacimiento;
        _user.PasswordHash = Seguridad.Encriptar("password");
        AspNetUsers userUpdate = await _CalendarServices.PostUserUpdAsync(_user);

        AspNetUserRoles _role = new AspNetUserRoles();
        _role.UserId = model.Id;
        _role.RoleId = model.Role;
        AspNetUserRoles roleInsert = await _CalendarServices.PostAspNetUserRolesAddAsync(_role);

        UserCalendarType _team = new UserCalendarType();
        _team.UserName = model.UserName;

        if (model.Teams != null)
        {
            UserCalendarType teamDelete = await _CalendarServices.PostUserCalendarTypeDelAsync(_team);

            foreach (var item in model.Teams)
            {
                _team.CalendarTypeId = item;
                UserCalendarType teamInsert = await _CalendarServices.PostUserCalendarTypeAddAsync(_team);

                // Calendar Calendar = new Calendar();
                // Calendar.title = model.UserName + " - " + "birthday";
                // Calendar.EventTypeId = 1;
                // Calendar.type = "birthday";
                // Calendar.CalendarTypeId = item;
                // Calendar.description = "";
                // Calendar.CalendarTypeName = "";

                // string dd = model.FechaNacimiento.ToString().Split("/").GetValue(0).ToString();
                // string mm = model.FechaNacimiento.ToString().Split("/").GetValue(1).ToString();
                // var year = DateTime.Today.Year.ToString();
                // string birthdayYear = dd + "/" + mm + "/" + year;
                // string enteredDate = birthdayYear; // + " 12:00:00";


                // DateTime date = DateTime.Parse(enteredDate);
                // DateTime dateinicio = date;
                // DateTime datefin = dateinicio.AddDays(1);

                //// string strDateInicio = String.Format("{0:yyyy-MM-dd HH:mm:ss}", model.FechaNacimiento);
                // //DateTime dateinicioFormat = DateTime.ParseExact(strDateInicio, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                // //string strDateFin= String.Format("{0:yyyy-MM-dd HH:mm:ss}", datefin);
                // //DateTime datefinFormat = DateTime.ParseExact(strDateFin, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);


                // Calendar.start = dateinicio;
                // Calendar.end = datefin; 
                // Calendar.allDay = true;
                // Calendar.UserName = model.UserName;
                // Calendar.UserCreate = model.Id;
                // var respuestaBE = await _CalendarServices.PostEventAddAsync(Calendar);
            }
        }

        string? userNameLogin = HttpContext.User.Identities.FirstOrDefault()?.Name;
        IEnumerable<AspNetUsers> userListLogin = await _CalendarServices.GetUsersAsync(userNameLogin);
        var _userLogin = userListLogin.ToList().FirstOrDefault();
        IEnumerable<AspNetUserRoles> _userRoleListLogin = await _CalendarServices.GetAspNetUserRolesAsync(_userLogin.Id);
        ViewBag.IsAdmin = _userRoleListLogin.ToList().Where(x => x.Name == "ADM").Count() > 0;
        ViewBag.IsSignedIn = true;
        ViewBag.IsOk = true;

        //return View("Success", model);
        return RedirectToAction("Index", "Home");
    }


    [Route("User/EditUser")]
    [HttpGet]
    public async Task<IActionResult> EditUserAsync()
    {
        ViewBag.IsSignedIn = false;
        var _userModel = new AspNetUsersModel();

        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {
            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);




            //var _userModelDB = _mapper.Map<List<AspNetUsersModel>>(user.ToList());
            _userModel.Id = _user.Id;
            _userModel.FechaNacimientoStr = String.Format("{0:yyyy-MM-dd}", _user.FechaNacimiento);
            _userModel.Nombres = _user.Nombres;
            _userModel.ApellidoPaterno = _user.ApellidoPaterno;
            _userModel.ApellidoMaterno = _user.ApellidoMaterno;
            _userModel.PhoneNumber = _user.PhoneNumber;

            //IEnumerable<AspNetUserRoles> _userRoleList = await _CalendarServices.GetAspNetUserRolesAsync(_user.Id);
            _userModel.roleList = _userRoleList;
            _userModel.Role = _userRoleList.FirstOrDefault()?.RoleId;

            //IEnumerable<UserCalendarType> _userTeamsList = await _CalendarServices.GetUserCalendarTypeAsync(userName);
            _userModel.teamsList = _userTeamsList;



            IEnumerable<AspNetRoles> _roleList = await _CalendarServices.GetRolesAsync();
            ViewBag.roleList = _roleList;

            IEnumerable<CalendarType> _teamsList = await _CalendarServices.GetCalendarTypeAsync(0);
            ViewBag.teamsList = _teamsList;

            ViewBag.Id = _user.Id;
            ViewBag.FechaNacimientoStr = String.Format("{0:dd/MM/yyyy}", _user.FechaNacimiento);
            ViewBag.Nombres = _user.Nombres;
            ViewBag.ApellidoPaterno = _user.ApellidoPaterno;
            ViewBag.ApellidoMaterno = _user.ApellidoMaterno;
            ViewBag.PhoneNumber = _user.PhoneNumber;
            ViewBag.IsSignedIn = true;
            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
            ViewBag.UserName = userName;

        }


        return View(_userModel);
    }


    [Route("User/EditOrganization")]
    [HttpGet]
    public async Task<IActionResult> EditOrganizationAsync()
    {
        ViewBag.IsSignedIn = false;
        var _userModel = new AspNetUsersModel();

        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {
            IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(userName);
            var _user = userList.ToList().FirstOrDefault();

            //var _userModelDB = _mapper.Map<List<AspNetUsersModel>>(user.ToList());
            _userModel.Id = _user.Id;
            _userModel.FechaNacimientoStr = String.Format("{0:yyyy-MM-dd}", _user.FechaNacimiento);
            _userModel.Nombres = _user.Nombres;
            _userModel.ApellidoPaterno = _user.ApellidoPaterno;
            _userModel.ApellidoMaterno = _user.ApellidoMaterno;
            _userModel.PhoneNumber = _user.PhoneNumber;


            IEnumerable<AspNetUserRoles> _userRoleList = await _CalendarServices.GetAspNetUserRolesAsync(_user.Id);
            _userModel.roleList = _userRoleList;
            _userModel.Role = _userRoleList.ToList().Where(x => x.Name != "ADM").FirstOrDefault()?.RoleId;


            IEnumerable<UserCalendarType> _userTeamsList = await _CalendarServices.GetUserCalendarTypeAsync(userName);
            var userTeamsList = _userTeamsList.ToList();
            _userModel.teamsList = userTeamsList;


            IEnumerable<AspNetRoles> _roleList = await _CalendarServices.GetRolesAsync();
            ViewBag.roleList = _roleList;

            IEnumerable<CalendarType> _teamsList = await _CalendarServices.GetCalendarTypeAsync(0);
            var teamsList = _teamsList.ToList();
            foreach (var item in teamsList)
            {
                if (teamsList.Count > 0)
                {
                    var list = userTeamsList.Where(d => d.CalendarTypeId == item.Id);
                    if (list.Count() > 0)
                        item.IsChecked = true;
                }
            }
            ViewBag.teamsList = teamsList;
            ViewBag.IsAdmin = _userRoleList.ToList().Where(x => x.Name == "ADM").Count() > 0;
            ViewBag.IsSignedIn = true;
            ViewBag.UserName = userName;
        }


        return View(_userModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}

