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
using AgileProject.Services.Implementaciones;

namespace AgileProject.Controllers;

public class SettingsController : Controller
{
    private readonly IConfiguration _config;
    ICalendarServices _CalendarServices;
    IClaimsServices _ClaimsServices;
    private readonly ILogger<SettingsController> _logger;
    private IMapper _mapper;

    string? _urlApiSeguridad;
    string? _rutaApiSeguridad;


    public SettingsController(
        IConfiguration config,
        ICalendarServices AgileProjectServices,
        IClaimsServices ClaimsServices,
        ILogger<SettingsController> logger,
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

            ViewBag.UserName = userName;
            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);

            ViewBag.IsSignedIn = true;
        }


        return View(_userModel);
    }


    [Route("Settings/ListTeams")]
    [HttpGet]
    public async Task<IActionResult> ListTeams()
    {



        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {

            List<CalendarTypeModel> userModelList = new List<CalendarTypeModel>();
            IEnumerable<CalendarType> _teamsList = await _CalendarServices.GetCalendarTypeAsync(0);

            int i = 0;
            foreach (var item in _teamsList)
            {
                var _userModel = new CalendarTypeModel();
                i++;
                _userModel.CustomId = i;
                _userModel.Id = item.Id;
                _userModel.Name = item.Name;
                _userModel.GroupType = item.GroupType;
                userModelList.Add(_userModel);

            }

            ViewBag.teamsList = userModelList;

            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
            ViewBag.IsSignedIn = true;
            ViewBag.UserName = userName;

        }


        return View();
    }

    [Route("Settings/ListRoles")]
    [HttpGet]
    public async Task<IActionResult> ListRoles()
    {


        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {


            List<AspNetRolesModel> userModelList = new List<AspNetRolesModel>();
            IEnumerable<AspNetRoles> _roleList = await _CalendarServices.GetRolesAsync();

            int i = 0;
            foreach (var item in _roleList)
            {
                var _userModel = new AspNetRolesModel();
                i++;
                _userModel.CustomId = i;
                _userModel.Id = item.Id;
                _userModel.Name = item.Name;
                _userModel.NormalizedName = item.NormalizedName;

                userModelList.Add(_userModel);

            }

            ViewBag.roleList = userModelList;

            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
            ViewBag.IsSignedIn = true;
            ViewBag.UserName = userName;
        }


        return View();
    }

    [Route("Settings/ListUsers")]
    [HttpGet]
    public async Task<IActionResult> ListUsers()
    {

        List<AspNetUsersModel> userModelList = new List<AspNetUsersModel>();

        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {
            IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(String.Empty);
            userList = userList.Where(x => x.UserName != "webmaster");

            int i = 0;
            foreach (var itemUser in userList)
            {
                var _userModel = new AspNetUsersModel();
                i++;

                _userModel.CustomId = i;
                _userModel.Id = itemUser.Id;
                _userModel.FechaNacimiento = itemUser.FechaNacimiento;
                _userModel.FechaNacimientoStr = String.Format("{0:yyyy-MM-dd}", itemUser.FechaNacimiento);
                _userModel.Nombres = itemUser.Nombres;
                _userModel.ApellidoPaterno = itemUser.ApellidoPaterno;
                _userModel.ApellidoMaterno = itemUser.ApellidoMaterno;
                _userModel.PhoneNumber = itemUser.PhoneNumber;
                _userModel.Email = itemUser.Email;
                _userModel.UserName = itemUser.UserName;

                //debe traer desde el select a la DB
                IEnumerable<UserCalendarType> _userTeams = await _CalendarServices.GetUserCalendarTypeAsync(itemUser.UserName);
                var strTeamsList = string.Empty;
                foreach (var item in _userTeams)
                {
                    var desc = string.Empty;
                    //if (item.CalendarTypeId == 1)
                    //    desc = "Squad";
                    //if (item.CalendarTypeId == 2)
                    //    desc = "Chapter";
                    //strTeamsList += desc + " " + item.Name + ",";

                    strTeamsList += item.Name + ",";
                }

                strTeamsList = RemoveEnd(strTeamsList, 1);
                _userModel.TeamsListStr = strTeamsList;


                userModelList.Add(_userModel);
            }


            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);


            ViewBag.userTeamsList = _userTeamsList;
            ViewBag.userList = userModelList;
            ViewBag.UserName = userName;

            ViewBag.IsSignedIn = true;
        }


        return View();
    }


    [Route("Settings/NewUser")]
    [HttpGet]
    public async Task<IActionResult> NewUser()
    {
        var _userModel = new AspNetUsersModel();
        List<AspNetUsersModel> userModelList = new List<AspNetUsersModel>();
        //var user = id;

        string? userName = string.Empty; // HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {
            IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(String.Empty);

            int i = 0;
            foreach (var item in userList)
            {
                _userModel = new AspNetUsersModel();
                i++;
                _userModel.CustomId = i;
                _userModel.Id = item.Id;
                _userModel.FechaNacimiento = item.FechaNacimiento;
                _userModel.FechaNacimientoStr = String.Format("{0:yyyy-MM-dd}", item.FechaNacimiento);
                _userModel.Nombres = item.Nombres;
                _userModel.ApellidoPaterno = item.ApellidoPaterno;
                _userModel.ApellidoMaterno = item.ApellidoMaterno;
                _userModel.PhoneNumber = item.PhoneNumber;
                _userModel.Email = item.Email;
                _userModel.UserName = item.UserName;

                userModelList.Add(_userModel);
            }

            var userTeamsList = new List<UserCalendarType>();
            _userModel = new AspNetUsersModel();


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

            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
            ViewBag.UserName = HttpContext.User.Identities.FirstOrDefault()?.Name; ;

            ViewBag.IsSignedIn = true;

        }



        return View("User", _userModel);
    }


    [Route("Settings/EditUser")]
    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var _userModel = new AspNetUsersModel();
        List<AspNetUsersModel> userModelList = new List<AspNetUsersModel>();
        //var user = id;

        string? userName = string.Empty; // HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {
            IEnumerable<AspNetUsers> userList = await _CalendarServices.GetUsersAsync(String.Empty);

            int i = 0;
            foreach (var item in userList)
            {
                _userModel = new AspNetUsersModel();
                i++;
                _userModel.CustomId = i;
                _userModel.Id = item.Id;
                _userModel.FechaNacimiento = item.FechaNacimiento;
                _userModel.FechaNacimientoStr = String.Format("{0:yyyy-MM-dd}", item.FechaNacimiento);
                _userModel.Nombres = item.Nombres;
                _userModel.ApellidoPaterno = item.ApellidoPaterno;
                _userModel.ApellidoMaterno = item.ApellidoMaterno;
                _userModel.PhoneNumber = item.PhoneNumber;
                _userModel.Email = item.Email;
                _userModel.UserName = item.UserName;

                userModelList.Add(_userModel);
            }

            var userTeamsList = new List<UserCalendarType>();
            if (id != null)
            {
                _userModel = userModelList.Where(x => x.Id == id).FirstOrDefault();

                IEnumerable<AspNetUserRoles> _userRoleListSelect = await _CalendarServices.GetAspNetUserRolesAsync(_userModel.Id);
                _userModel.roleList = _userRoleListSelect;
                _userModel.Role = _userRoleListSelect.ToList().Where(x => x.Name != "ADM").FirstOrDefault()?.RoleId;


                IEnumerable<UserCalendarType> _userTeamsListSelect = await _CalendarServices.GetUserCalendarTypeAsync(_userModel.UserName);
                userTeamsList = _userTeamsListSelect.ToList();
                _userModel.teamsList = _userTeamsListSelect;
            }
            else
            {
                _userModel = new AspNetUsersModel();
            }


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

            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
            ViewBag.UserName = HttpContext.User.Identities.FirstOrDefault()?.Name; ;
            ViewBag.IsSignedIn = true;
        }



        return View("User", _userModel);
    }

    [Route("Settings/SaveUser")]
    [HttpPost]
    public async Task<IActionResult> SaveUserAsync(AspNetUsersModel model)
    {
        string? userNameSession = HttpContext.User.Identities.FirstOrDefault()?.Name;

        AspNetUsers _user = new AspNetUsers();
        _user.Id = model.Id;
        _user.Nombres = model.Nombres;
        _user.ApellidoPaterno = model.ApellidoPaterno;
        _user.ApellidoMaterno = model.ApellidoMaterno;

        _user.FechaNacimiento = model.FechaNacimiento;
        model.FechaNacimientoStr = String.Format("{0:yyyy-MM-dd}", model.FechaNacimiento);
        _user.Email = model.Email;
        _user.UserName = model.Email;


        AspNetUserRoles _role = new AspNetUserRoles();
        _role.UserId = model.Id;
        _role.RoleId = model.Role;


        AspNetUsers userRpta = new AspNetUsers();
        if (String.IsNullOrEmpty(model.Id))
        {

            //user
            userRpta = await _CalendarServices.PostAspNetUserAddAsync(_user);

            //rol
            _role.UserId = userRpta.Id;
            var userRolRpta = await _CalendarServices.PostAspNetUserRolesAddAsync(_role);

            if (model.Teams != null)
            {
                //UserCalendarType teamDelete = await _CalendarServices.PostUserCalendarTypeDelAsync(_team);

                foreach (var item in model.Teams)
                {
                    //team
                    UserCalendarType _team = new UserCalendarType();
                    _team.UserName = _user.Email;
                    _team.CalendarTypeId = item;
                    UserCalendarType teamInsert = await _CalendarServices.PostUserCalendarTypeAddAsync(_team);

                    ////birthday
                    //string dd = model.FechaNacimiento.ToString().Split("/").GetValue(0).ToString();
                    //string mm = model.FechaNacimiento.ToString().Split("/").GetValue(1).ToString();
                    //var year = DateTime.Today.Year.ToString();
                    //string birthdayYear = dd + "/" + mm + "/" + year;
                    //string enteredDate = birthdayYear; // + " 12:00:00";

                    //DateTime fecNac = DateTime.Parse(enteredDate);
                    //DateTime dateinicio = fecNac;
                    //DateTime datefin = dateinicio.AddDays(1);

                    //Calendar Calendar = new Calendar();
                    //Calendar.title = _user.Email + " - " + "birthday";
                    //Calendar.EventTypeId = 1;
                    //Calendar.type = "birthday";
                    //Calendar.CalendarTypeId = item;
                    //Calendar.description = "";
                    //Calendar.CalendarTypeName = "";
                    //Calendar.start = dateinicio;
                    //Calendar.end = datefin;
                    //Calendar.allDay = true;
                    //Calendar.UserName = _user.Email;
                    //Calendar.UserCreate = model.Id;
                    //var respuestaBE = await _CalendarServices.PostEventAddAsync(Calendar);
                }
            }

        }
        else
        {
            //user
            userRpta = await _CalendarServices.PostUserUpdAsync(_user);

            //rol
            AspNetUserRoles roleDel = await _CalendarServices.PostAspNetUserRolesDelAsync(_role);
            AspNetUserRoles roleInsert = await _CalendarServices.PostAspNetUserRolesAddAsync(_role);

            //team
            UserCalendarType _team = new UserCalendarType();
            _team.UserName = model.Email;

            UserCalendarType teamDelete = await _CalendarServices.PostUserCalendarTypeDelAsync(_team);
            if (model.Teams != null)
            {
                foreach (var item in model.Teams)
                {
                    _team.CalendarTypeId = item;
                    UserCalendarType teamInsert = await _CalendarServices.PostUserCalendarTypeAddAsync(_team);
                }
            }

        }


        //return model
        IEnumerable<AspNetRoles> _roleList = await _CalendarServices.GetRolesAsync();
        ViewBag.roleList = _roleList;

        IEnumerable<CalendarType> _teamsList = await _CalendarServices.GetCalendarTypeAsync(0);
        var teamsList = _teamsList.ToList();
        foreach (var item in teamsList)
        {
            if (teamsList.Count > 0)
            {
                var list = model.Teams.Where(d => d == item.Id);
                if (list.Count() > 0)
                    item.IsChecked = true;
            }
        }
        ViewBag.teamsList = teamsList;


        var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
        var _userLogin = new AspNetUsersModel();
        _userLogin = _ClaimsServices.getUserClaims(claims);

        List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
        _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

        List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
        _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

        ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
        ViewBag.UserName = userNameSession;
        ViewBag.IsSignedIn = true;
        ViewBag.IsOk = true;

        return View("User", model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



    [Route("Settings/NewTeam")]
    [HttpGet]
    public IActionResult NewTeam()
    {
        var _Teams = new CalendarTypeModel();
        //_Teams.Id = -1;

        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
        var _user = new AspNetUsersModel();
        _user = _ClaimsServices.getUserClaims(claims);

        List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
        _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

        List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
        _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

        ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
        ViewBag.IsSignedIn = true;
        ViewBag.UserName = userName;

        return View("Teams", _Teams);
    }

    [Route("Settings/NewRole")]
    [HttpGet]
    public IActionResult NewRole()
    {
        var _Roles = new AspNetRolesModel();
        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;

        var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
        var _user = new AspNetUsersModel();
        _user = _ClaimsServices.getUserClaims(claims);

        List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
        _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

        List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
        _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

        ViewBag.UserName = userName;

        ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
        ViewBag.IsSignedIn = true;

        return View("Roles", _Roles);
    }


    [Route("Settings/EditRole")]
    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
    {

        var model = new AspNetRolesModel();

        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {

            int _id = Convert.ToInt32(id);
            var _role = await _CalendarServices.GetAspNetRolesAsync(_id);
            model.Id = _role.Id;
            model.Name = _role.Name;
            model.NormalizedName = _role.NormalizedName;

            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

            ViewBag.UserName = userName;
            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
            ViewBag.IsSignedIn = true;

        }

        return View("Roles", model);

    }

    [Route("Settings/SaveRole")]
    [HttpPost]
    public async Task<IActionResult> SaveRole(AspNetRolesModel model)
    {
        var _roleRpta = new AspNetRoles();
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

            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);

            AspNetRoles _role = new AspNetRoles();
            _role.Name = model.Name;
            _role.NormalizedName = model.NormalizedName;

            if (model.Id != 0)
            {
                _role.Id = model.Id;
                _roleRpta = await _CalendarServices.PostAspNetRolesUpdAsync(_role);
            }
            else
            {
                _roleRpta = await _CalendarServices.PostAspNetRolesAddAsync(_role);
            }

            ViewBag.UserName = userName;
            ViewBag.IsSignedIn = true;
        }

        ViewBag.IsOk = true;

        return View("Roles", model);

    }

    [Route("Settings/EditTeam")]
    [HttpGet]
    public async Task<IActionResult> EditTeam(string id)
    {

        var model = new CalendarTypeModel();

        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {

            int _id = Convert.ToInt32(id);
            var _teamList = await _CalendarServices.GetCalendarTypeAsync(_id);
            var _team = _teamList.FirstOrDefault();
            model.Id = _team.Id;
            model.Name = _team.Name;
            model.GroupType = _team.GroupType;

            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var _user = new AspNetUsersModel();
            _user = _ClaimsServices.getUserClaims(claims);

            List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
            _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

            List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
            _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);

            ViewBag.UserName = userName;
            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
            ViewBag.IsSignedIn = true;
        }

        return View("Teams", model);

    }


    [Route("Settings/SaveTeam")]
    [HttpPost]
    public async Task<IActionResult> SaveTeam(CalendarTypeModel model)
    {
        var _teamRpta = new CalendarType();
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

            ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);

            CalendarType _team = new CalendarType();
            _team.Name = model.Name;
            _team.GroupType = model.GroupType;

            if (model.Id != 0)
            {
                _team.Id = model.Id;
                _teamRpta = await _CalendarServices.PostCalendarTypeUpdAsync(_team);
            }
            else
            {
   
                _teamRpta = await _CalendarServices.PostCalendarTypeAddAsync(_team);
                model.Id = _team.Id;
            }

            ViewBag.UserName = userName;
            ViewBag.IsSignedIn = true;
        }

        ViewBag.IsOk = true;

        return View("Teams", model);

    }



    public String RemoveEnd(String str, int len)
    {
        if (str.Length < len)
        {
            return string.Empty;
        }

        return str[..^len];
    }


}

