using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AgileProject.Entidades;
using AgileProject.Models;
using AgileProject.Services.Contratos;
using AgileProject;
//using System.Globalization;
using System.Diagnostics;
using AgileProject.Model;
using Newtonsoft.Json.Linq;
using AgileProject.Mapping;
using AutoMapper;
using AgileProject.Services.Implementaciones;
using System.Security.Claims;
using Microsoft.VisualBasic;


namespace AgileProject.Controllers
{

    public class CalendarController : Controller
    {
        private readonly IConfiguration _config;
        ICalendarServices _CalendarServices;
        IClaimsServices _ClaimsServices;
        private readonly ILogger<CalendarController> _logger;
        private IMapper _mapper;

        string? _urlApiSeguridad;
        string? _rutaApiSeguridad;
        CalendarModel calendarModel = new CalendarModel();

        public List<EventTypeModel>? EvenTypeList { get; set; }
        public List<CalendarModel>? CalendarList { get; set; }

        public CalendarController(
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


            ViewBag.IsSignedIn = false;

            try
            {

                var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
                bool validClaims = _ClaimsServices.validClaims(claims);
                if (validClaims)
                {
                    //user
                    var _userLogin = new AspNetUsersModel();
                    _userLogin = _ClaimsServices.getUserClaims(claims);

                    //roles
                    List<AspNetUserRoles> _userRoleList = new List<AspNetUserRoles>();
                    _userRoleList = _ClaimsServices.getUserRoleClaims(claims);

                    //teams
                    List<UserCalendarType> _userTeamsList = new List<UserCalendarType>();
                    _userTeamsList = _ClaimsServices.getUserTeamsClaims(claims);


                    ViewBag.IsAdmin = _ClaimsServices.isAdmin(claims);
                    ViewBag.userTeamsList = _userTeamsList;

                    //if (_userTeamsList.Count == 0)
                    //{
                    //    return RedirectToAction("Index", "User");
                    //}

                    //tipos de calendario
                    IEnumerable<EventType> getEventTypesAsync = await _CalendarServices.GetEventTypesAsync();
                    var evenTypeList = _mapper.Map<List<EventTypeModel>>(getEventTypesAsync.ToList());
                    //var rr = _mapper.Map<EntidadOrigen>(EntidadDestino);
                    calendarModel.EvenTypeList = evenTypeList;

                    var mesActual = DateTime.Now.Month;
                    //mesActual = "/11/";

                    //cumpleaños este mes
                    string str = String.Join(",", _userTeamsList.Select(c => c.CalendarTypeId).ToList());
                    IEnumerable<Calendar> getCalendarAsync = await _CalendarServices.GetCalendarAsync(str);
                    getCalendarAsync = getCalendarAsync.Where(x => x.start.Month == mesActual && x.EventTypeId == 1);
                    var listCalendar = _mapper.Map<List<CalendarModel>>(getCalendarAsync.ToList());
                    listCalendar.ForEach(x => x.Title = x.Title.Replace("Cumpleaños", string.Empty).Replace("Cumpleaños", string.Empty).Replace("Cumple", string.Empty).Replace(" de ", string.Empty));
                    ViewBag.CalendarList = listCalendar;
                    


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
                    return RedirectToAction("Login", "Cuenta");
                }

            }
            catch (HttpRequestException exception)
            {
                _logger.LogError("An HTTP request exception occurred. {0}", exception.Message);

            }

            return View(calendarModel);
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

       

        [Route("Calendar/GetEventTypesAsync")]
        [HttpGet]
        public async Task<IActionResult> GetEventTypesAsync()
        {

            var respuestaBE = await _CalendarServices.GetEventTypesAsync();
            return Ok(respuestaBE);
        }


        [Route("Calendar/PostEventAddAsync")]
        [HttpPost]
        public async Task<IActionResult> PostEventAddAsync([FromBody] Calendar Calendar)
        {

            var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            //user
            var _userLogin = new AspNetUsersModel();
            _userLogin = _ClaimsServices.getUserClaims(claims);

            Calendar.UserName = _userLogin.UserName;
            Calendar.UserCreate = _userLogin.Id;
            var respuestaBE = await _CalendarServices.PostEventAddAsync(Calendar);

            return Ok(respuestaBE);
        }

        [Route("Calendar/PostEventUpdAsync")]
        [HttpPost]
        public async Task<IActionResult> PostEventUpdAsync([FromBody] Calendar Calendar)
        {

            var respuestaBE = await _CalendarServices.PostEventUpdAsync(Calendar);
            return Ok(respuestaBE);
        }

        [Route("Calendar/PostEventDelAsync")]
        [HttpPost]
        public async Task<IActionResult> PostEventDelAsync([FromBody] Calendar Calendar)
        {

            var respuestaBE = await _CalendarServices.PostEventDelAsync(Calendar);
            return Ok(respuestaBE);
        }


        [Route("Calendar/GetCalendarAsync")]
        [HttpGet]
        public async Task<IActionResult> GetCalendarAsync(string idList)
        {
            //List<string> idList = new List<string>();
            var respuestaBE = await _CalendarServices.GetCalendarAsync(idList);
            return Ok(respuestaBE);
        }


    }
}
