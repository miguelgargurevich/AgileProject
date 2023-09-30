
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

namespace AgileProject.Repository.Implementaciones
{

    public class CalendarRepositoryOld 
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<CalendarRepository> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        string? connectionString = String.Empty;


        public CalendarRepositoryOld(IConfiguration configuration, ILogger<CalendarRepository> logger,
            IWebHostEnvironment webHostEnvironment, ApplicationDbContext context, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _mapper = mapper;

            connectionString = _configuration.GetConnectionString("dbDocker"); 
        }

        #region "Calendario"

        public IEnumerable<EventType> GetEventTypes() 
        {
            
            List<EventType> list = new List<EventType>();
            var queryString = "select * from [dbo].[EventType]";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var adapter = new SqlDataAdapter(queryString, conn))
                {
                    conn.Open();
                    var reader = adapter.SelectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        EventType obj = new EventType();
                        obj.Id = reader.GetInt32(0);
                        obj.Name = reader.GetString(1);
                        obj.Color = reader.GetString(2);
                        list.Add(obj);
                    }
                    
                }
            }

            return list;
        }

        public async Task<IEnumerable<EventType>> GetEventTypesAsync()
        {

            List<EventType> list = new List<EventType>();
            var queryString = "select Id, Name, Color from [dbo].[EventType]";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, connection))
                    {
                        await connection.OpenAsync();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            EventType myevent = new EventType();
                            myevent.Id = reader.GetInt32(0);
                            myevent.Name = reader.GetString(1);
                            myevent.Color = reader.GetString(2);
                            list.Add(myevent);
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                CapturarError(ex, "Repository", "GetUsersAsync");

            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetUsersAsync");

            }

            return list.ToArray();
        }

        public async Task<Calendar> PostEventAddAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();

            _context.Calendar.Add(Calendar);
            _ = await _context.SaveChangesAsync();
            list.id = Calendar.id;

            return list;

        }

        public async Task<Calendar> PostEventAddAsyncOld(Calendar Calendar)
        {
            Calendar list = new Calendar();
            
            string _query = "INSERT INTO [Calendar] (Title,start,[end],AllDay,EventTypeId,EventTypeName,CalendarTypeId,CalendarTypeName,Description,UserCreate,DateCreate,UserName) " +
                "values (@title,@start,@end,@allDay,@eventTypeId,@eventTypeName,@calendarTypeId,@calendarTypeName,@description,@userCreate,@dateCreate,@userName)" +
                " Set @id = Scope_Identity();";
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    //comm.Parameters.AddWithValue("@id", Calendar.id);
                    comm.Parameters.AddWithValue("@title", Calendar.title);
                    comm.Parameters.AddWithValue("@start", Calendar.start);
                    comm.Parameters.AddWithValue("@end", Calendar.end);
                    comm.Parameters.AddWithValue("@allDay", Calendar.allDay);
                    comm.Parameters.AddWithValue("@eventTypeId", Calendar.EventTypeId);
                    comm.Parameters.AddWithValue("@eventTypeName", Calendar.type);
                    comm.Parameters.AddWithValue("@calendarTypeId", Calendar.CalendarTypeId);
                    comm.Parameters.AddWithValue("@calendarTypeName", Calendar.CalendarTypeName);
                    comm.Parameters.AddWithValue("@description", Calendar.description);
                    comm.Parameters.AddWithValue("@userCreate", Calendar.UserCreate);
                    comm.Parameters.AddWithValue("@dateCreate", DateTime.Now);
                    comm.Parameters.AddWithValue("@userName", Calendar.UserName);
                    comm.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                        int returnID = (int)comm.Parameters["@id"].Value;
                        list.id = returnID;
                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostEventAddAsync");
                        
                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostEventAddAsync");
                        
                    }
                }
            }

            return list;

        }

        public async Task<CalendarType> PostCalendarTypeAddAsync(CalendarType CalendarType)
        {
            CalendarType list = new CalendarType();

            string _query = "INSERT INTO [CalendarType] (name,groupType) " +
                "values (@name,@groupType)" +
                " Set @id = Scope_Identity();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    //comm.Parameters.AddWithValue("@id", Calendar.id);
                    comm.Parameters.AddWithValue("@name", CalendarType.Name);
                    comm.Parameters.AddWithValue("@groupType", CalendarType.GroupType);
                    comm.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                        int returnID = (int)comm.Parameters["@id"].Value;
                        list.Id = returnID;
                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostCalendarTypeAddAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostCalendarTypeAddAsync");

                    }
                }
            }

            return list;

        }

        public async Task<CalendarType> PostCalendarTypeUpdAsync(CalendarType CalendarType)
        {
            CalendarType list = new CalendarType();

            string _query = "" +
                "UPDATE CalendarType SET name = @name, groupType = @groupType WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@id", CalendarType.Id);
                    comm.Parameters.AddWithValue("@name", CalendarType.Name);
                    comm.Parameters.AddWithValue("@groupType", CalendarType.GroupType);

                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostCalendarTypeUpdAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostCalendarTypeUpdAsync");

                    }
                }
            }

            return list;

        }


        public async Task<Calendar> PostEventUpdAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();
            
            string _query = "" +
                "UPDATE Calendar SET title = @title, start = @start, end = @end, description = @description, eventtypeid = @eventtypeid, eventtypename = @eventtypename, calendartypeid = @calendartypeid,calendartypename = @calendartypename, allday = @allday WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@id", Calendar.id);
                    comm.Parameters.AddWithValue("@title", Calendar.title);
                    comm.Parameters.AddWithValue("@start", Calendar.start);
                    comm.Parameters.AddWithValue("@end", Calendar.end);
                    comm.Parameters.AddWithValue("@allDay", Calendar.allDay);
                    comm.Parameters.AddWithValue("@eventTypeId", Calendar.EventTypeId);
                    comm.Parameters.AddWithValue("@eventTypeName", Calendar.type);
                    comm.Parameters.AddWithValue("@calendarTypeId", Calendar.CalendarTypeId);
                    comm.Parameters.AddWithValue("@calendarTypeName", Calendar.CalendarTypeName);
                    comm.Parameters.AddWithValue("@description", Calendar.description);
                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostEventUpdAsync");
                        
                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostEventUpdAsync");

                    }
                }
            }

            return list;

        }

        public async Task<Calendar> PostEventDelAsync(Calendar Calendar)
        {
            Calendar list = new Calendar();
            
            string _query = "" +
                "Delete from Calendar WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@id", Calendar.id);
                   
                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostEventDelAsync");
                        
                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostEventDelAsync");

                    }
                }
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

                list = await queryResult.ToListAsync(); // Cambio aquí a método asincrónico.

                // userModel = _mapper.Map<AspNetUsers>(user); //map ok just missing register in mappingProfile class
            }
            catch (Exception e)
            {
                CapturarError(e);
            }

            return list;
        }


        public async Task<IEnumerable<Calendar>> GetCalendarAsyncOld(string idList)
        {
            List<Calendar> list = new List<Calendar>();
            string where = "";

            string queryString = "SELECT a.id, a.title, a.start, a.[end], a.description, a.eventtypeid, a.calendartypeid, " +
                " a.allday, b.color, ISNULL(a.UserName,'') as UserName, b.Name " +
                "FROM  dbo.Calendar a inner join dbo.EventType b on b.id = a.eventtypeid ";

            if (idList != null)
                where = "where a.calendartypeid IN (" + idList + ")";
            
            queryString = queryString + where;

            try {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, conn))
                    {
                        conn.Open();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            Calendar obj = new Calendar();
                            obj.id = reader.GetInt32(0);
                            obj.title = reader.GetString(1);
                            obj.start = reader.GetDateTime(2);
                            obj.end = reader.GetDateTime(3);
                            obj.description = reader.GetString(4);
                            obj.EventTypeId = reader.GetInt32(5);
                            obj.CalendarTypeId = reader.GetInt32(6);
                            obj.allDay = Convert.ToBoolean(reader.GetBoolean(7));
                            obj.color = reader.GetString(8);
                            obj.UserName = reader.GetString(9);
                            obj.type = reader.GetString(10);
                            list.Add(obj);
                        }

                    }
                }
            }
            catch (SqlException ex) 
            { 
                CapturarError(ex, "Repository", "GetCalendarAsync");
            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetCalendarAsync");

            }


            return list;

        }

        #endregion

        #region "Usuarios"

        public async Task<IEnumerable<AspNetUsers>> GetUsersAsync(string userName)
        {
            //System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es");
            List<AspNetUsers> list = new List<AspNetUsers>();
            var where = "a.UserName";
            if (!String.IsNullOrEmpty(userName))
                where = "'" + userName + "'";


            var queryString = "select ROW_NUMBER() OVER (ORDER BY a.Id), a.Id, ISNULL(a.Email,''),ISNULL(a.PhoneNumber,''),a.UserName,ISNULL(a.Nombres,''),ISNULL(a.ApellidoPaterno,''),ISNULL(a.ApellidoMaterno,''),ISNULL(a.FechaNacimiento, '1900-01-01') " + 
                "from AspNetUsers a " +
                "where a.UserName = " + where;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, connection))
                    {
                        await connection.OpenAsync();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            AspNetUsers myevent = new AspNetUsers();
                            //myevent.CustomId = (int)reader.GetInt64(0); 
                            myevent.Id = reader.GetString(1);
                            myevent.Email = reader.GetString(2);
                            myevent.PhoneNumber = reader.GetString(3);
                            myevent.UserName = reader.GetString(4);
                            myevent.Nombres = reader.GetString(5);
                            myevent.ApellidoPaterno = reader.GetString(6);
                            myevent.ApellidoMaterno = reader.GetString(7);
                            myevent.FechaNacimiento = reader.GetDateTime(8);
                               
                                
                            list.Add(myevent);
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                CapturarError(ex, "Repository", "GetUsersAsync");

            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetUsersAsync");

            }

            return list.ToArray();
        }

        public async Task<IEnumerable<AspNetRoles>> GetRolesAsync()
        {

            List<AspNetRoles> list = new List<AspNetRoles>();
            var queryString = "select Id, Name, NormalizedName, ISNULL(ConcurrencyStamp,'') " +
                "from AspNetRoles " +
                "where Name != 'ADM' ";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, connection))
                    {
                        await connection.OpenAsync();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            AspNetRoles myevent = new AspNetRoles();
                            //myevent.Id = reader.GetString(0);
                            myevent.Name = reader.GetString(1);
                            myevent.NormalizedName = reader.GetString(2);
                            myevent.ConcurrencyStamp = reader.GetString(3);
                            
                            list.Add(myevent);
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                CapturarError(ex, "Repository", "GetRolesAsync");

            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetRolesAsync");

            }

            return list.ToArray();
        }
          
        public async Task<IEnumerable<CalendarType>> GetCalendarTypeAsync()
        {

            List<CalendarType> list = new List<CalendarType>();
            var queryString = "select Id, Name, GroupType " +
                "from CalendarType ";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, connection))
                    {
                        await connection.OpenAsync();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            CalendarType myevent = new CalendarType();
                            myevent.Id = reader.GetInt32(0);
                            myevent.Name = reader.GetString(1);
                            myevent.GroupType = reader.GetString(2);
                          
                            list.Add(myevent);
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                CapturarError(ex, "Repository", "GetCalendarTypeAsync");

            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetCalendarTypeAsync");

            }

            return list.ToArray();
        }

        public async Task<AspNetUsers> PostUserUpdAsync(AspNetUsers userBE)
        {
            AspNetUsers list = new AspNetUsers();
            var campos = "";
            var where = "where Id = @Id ";
            string _query = "UPDATE AspNetUsers SET UserName = @UserName ";
            if (userBE.Email != null) campos += ", Email = @Email ";
            if (userBE.Nombres != null) campos += ", Nombres = @Nombres ";
            if (userBE.ApellidoPaterno != null) campos += ", ApellidoPaterno = @ApellidoPaterno ";
            if (userBE.ApellidoMaterno != null) campos += ",ApellidoMaterno = @ApellidoMaterno ";
            if (userBE.FechaNacimiento != null) campos += ", FechaNacimiento = @FechaNacimiento ";
            if (userBE.PhoneNumber != null) campos += ", PhoneNumber = @PhoneNumber ";

            _query = _query + campos + where;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    if (userBE.Id != null) comm.Parameters.AddWithValue("@Id", userBE.Id);
                    if (userBE.UserName != null) comm.Parameters.AddWithValue("@Email", userBE.UserName);
                    if (userBE.Nombres != null) comm.Parameters.AddWithValue("@Nombres", userBE.Nombres);
                    if (userBE.ApellidoPaterno != null) comm.Parameters.AddWithValue("@ApellidoPaterno", userBE.ApellidoPaterno);
                    if (userBE.ApellidoMaterno != null) comm.Parameters.AddWithValue("@ApellidoMaterno", userBE.ApellidoMaterno);
                    if (userBE.FechaNacimiento != null) comm.Parameters.AddWithValue("@FechaNacimiento", userBE.FechaNacimiento);
                    if (userBE.PhoneNumber != null) comm.Parameters.AddWithValue("@PhoneNumber", userBE.PhoneNumber);

                    comm.Parameters.AddWithValue("@UserName", userBE.UserName);
                    //comm.Parameters.AddWithValue("@Id", userBE.Id);
                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostUserUpdAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostUserUpdAsync");

                    }
                }
            }

            return list;

        }
       
        public async Task<UserCalendarType> PostUserCalendarTypeAddAsync(UserCalendarType UserCalendarType)
        {
            UserCalendarType list = new UserCalendarType();

            string _query = "INSERT INTO [UserCalendarType] (UserName,CalendarTypeId) " +
                "values(@UserName, @CalendarTypeId)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    //comm.Parameters.AddWithValue("@id", Calendar.id);
                    comm.Parameters.AddWithValue("@UserName", UserCalendarType.UserName);
                    comm.Parameters.AddWithValue("@CalendarTypeId", UserCalendarType.CalendarTypeId);
                   
                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostUserCalendarTypeAddAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostUserCalendarTypeAddAsync");

                    }
                }
            }

            return list;

        }

        public async Task<UserCalendarType> PostUserCalendarTypeDelAsync(UserCalendarType UserCalendarType)
        {
            UserCalendarType list = new UserCalendarType();

            string _query = "DELETE FROM [UserCalendarType] WHERE UserName = @UserName; " ;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    //comm.Parameters.AddWithValue("@id", Calendar.id);
                    comm.Parameters.AddWithValue("@UserName", UserCalendarType.UserName);

                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostUserCalendarTypeDelAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostUserCalendarTypeDelAsync");

                    }
                }
            }

            return list;

        }

        public async Task<AspNetUserRoles> PostAspNetUserRolesDelAsync(AspNetUserRoles aspNetUserRoles)
        {
            AspNetUserRoles list = new AspNetUserRoles();

            string _query = "DELETE FROM [AspNetUserRoles] WHERE UserId = @UserId AND RoleId != 0";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@UserId", aspNetUserRoles.UserId);

                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetUserRolesDelAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetUserRolesDelAsync");

                    }
                }
            }

            return list;

        }

        public async Task<AspNetUserRoles> PostAspNetUserRolesAddAsync(AspNetUserRoles aspNetUserRoles)
        {
            AspNetUserRoles list = new AspNetUserRoles();

            string _query = "INSERT INTO [AspNetUserRoles] (UserId,RoleId) " +
                "values(@UserId, @RoleId)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@UserId", aspNetUserRoles.UserId);
                    comm.Parameters.AddWithValue("@RoleId", aspNetUserRoles.RoleId);

                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetUserRolesAddAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetUserRolesAddAsync");

                    }
                }
            }

            return list;

        }

        public async Task<IEnumerable<AspNetUserRoles>> GetAspNetUserRolesAsync(string userId)
        {

            List<AspNetUserRoles> list = new List<AspNetUserRoles>();
            var queryString = "select a.UserId, b.UserName, CAST(a.RoleId AS int) AS RoleId, c.Name as RoleName, c.NormalizedName from AspNetUserRoles a " +
                            "inner join AspNetUsers b on b.id = a.userid " +
                            "inner join AspNetRoles c on c.Id = a.RoleId " +
                            "where a.UserId = '" + userId + "'";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, connection))
                    {
                        await connection.OpenAsync();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            AspNetUserRoles myevent = new AspNetUserRoles();
                            myevent.UserId = reader.GetString(0);
                            myevent.UserName = reader.GetString(1);
                            myevent.RoleId = reader.GetInt32(2);
                            myevent.Name = reader.GetString(3);
                            myevent.NormalizedName = reader.GetString(4);
                            list.Add(myevent);
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                CapturarError(ex, "Repository", "GetUsersAsync");

            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetUsersAsync");

            }

            return list.ToArray();
        }

        public async Task<IEnumerable<UserCalendarType>> GetUserCalendarTypeAsync(string userName)
        {
            //System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es");

            List<UserCalendarType> list = new List<UserCalendarType>();
            var queryString = "select a.UserName,a.CalendarTypeId,b.Name,b.groupType " +
                            "from UserCalendarType a " + 
                            "inner join CalendarType b on a.CalendarTypeId = b.id " +
                            "where a.UserName = '" + userName + "'";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, connection))
                    {
                        await connection.OpenAsync();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            UserCalendarType myevent = new UserCalendarType();
                            myevent.UserName = reader.GetString(0);
                            myevent.CalendarTypeId = reader.GetInt32(1);
                            myevent.Name = reader.GetString(2);
                            myevent.GroupType = reader.GetString(3);
                           
                            list.Add(myevent);
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                CapturarError(ex, "Repository", "GetUserCalendarTypeAsync");

            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetUserCalendarTypeAsync");

            }

            return list.ToArray();
        }


        public async Task<AspNetUsers> PostAspNetUserAddAsync(AspNetUsers AspNetUsers)
        {
            //AspNetUsers _AspNetUsers = new AspNetUsers();
            Guid obj = Guid.NewGuid();
            //var genPass = "AQAAAAEAACcQAAAAEEwFaE3V8mEGzrhV36TThPLx1GI7E7pc4Neq04qwNBk/qWJr1Da7OYLt3F5H1y4cXg=="; //Password1!
            var confirmedEmail = 1;

            AspNetUsers.Id = obj.ToString();
            //Console.WriteLine("New Guid is " + obj.ToString());

            string _query = "INSERT INTO AspNetUsers (Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento) " +
                "values (@guid,0,'22522292-f147-487c-af39-8e3809d255db',@email,@confirmedEmail,1,null,@username,@username,@genPass,null,0,'5UBJN54F7PXR6KDYQDEM74K7DZRP6VHQ',0,@username,@nombres,@apellidoPaterno,@apellidoMaterno,@fechaNacimiento)";


             using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@guid", AspNetUsers.Id);
                    comm.Parameters.AddWithValue("@email", AspNetUsers.Email);
                    comm.Parameters.AddWithValue("@confirmedEmail", confirmedEmail); 
                    comm.Parameters.AddWithValue("@username", AspNetUsers.Email);
                    comm.Parameters.AddWithValue("@genPass", AspNetUsers.PasswordHash);
                    comm.Parameters.AddWithValue("@nombres", AspNetUsers.Nombres);
                    comm.Parameters.AddWithValue("@apellidoPaterno",AspNetUsers.ApellidoPaterno);
                    comm.Parameters.AddWithValue("@apellidoMaterno", AspNetUsers.ApellidoMaterno);
                    comm.Parameters.AddWithValue("@fechaNacimiento",AspNetUsers.FechaNacimiento);

                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetUserAddAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetUserAddAsync");

                    }
                }
            }

            return AspNetUsers;

        }


        public async Task<AspNetRoles> GetAspNetRolesAsync(string id)
        {

            AspNetRoles myevent = new AspNetRoles();
            var queryString = "select a.Id,a.Name,a.NormalizedName " +
                            "from AspNetRoles a " +
                            "where a.Id = '" + id + "'";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, connection))
                    {
                        await connection.OpenAsync();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {

                            //myevent.Id = reader.GetString(0);
                            myevent.Name = reader.GetString(1);
                            myevent.NormalizedName = reader.GetString(2);
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                CapturarError(ex, "Repository", "GetAspNetRolesAsync");

            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetAspNetRolesAsync");

            }

            return myevent;
        }

        public async Task<CalendarType> GetCalendarTypeAsync(string id)
        {

            CalendarType myevent = new CalendarType();
            var queryString = "select a.Id,a.Name,a.GroupType " +
                            "from CalendarType a " +
                            "where a.Id = '" + id + "'";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var adapter = new SqlDataAdapter(queryString, connection))
                    {
                        await connection.OpenAsync();
                        var reader = await adapter.SelectCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            
                            myevent.Id = reader.GetInt32(0);
                            myevent.Name = reader.GetString(1);
                            myevent.GroupType = reader.GetString(2);

                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                CapturarError(ex, "Repository", "GetCalendarTypeAsync");

            }
            catch (Exception ex)
            {
                CapturarError(ex, "Repository", "GetCalendarTypeAsync");

            }

            return myevent;
        }


        public async Task<AspNetRoles> PostAspNetRolesAddAsync(AspNetRoles aspNetRoles)
        {
            AspNetRoles list = new AspNetRoles();

            string _query = "INSERT INTO [AspNetRoles] (Id,Name,NormalizedName) " +
                "values(@Id, @Name, @NormalizedName)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@Id", aspNetRoles.Id);
                    comm.Parameters.AddWithValue("@Name", aspNetRoles.Name);
                    comm.Parameters.AddWithValue("@NormalizedName", aspNetRoles.NormalizedName);
                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetRolesAddAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetRolesAddAsync");

                    }
                }
            }

            return list;

        }

        public async Task<AspNetRoles> PostAspNetRolesUpdAsync(AspNetRoles aspNetRoles)
        {
            AspNetRoles list = new AspNetRoles();

            string _query = "UPDATE [AspNetRoles] SET Name=@Name, NormalizedName=@NormalizedName where Id=@Id ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@Id", aspNetRoles.Id);
                    comm.Parameters.AddWithValue("@Name", aspNetRoles.Name);
                    comm.Parameters.AddWithValue("@NormalizedName", aspNetRoles.NormalizedName);
                    try
                    {
                        conn.Open();
                        await comm.ExecuteNonQueryAsync();

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetRolesUpdAsync");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "PostAspNetRolesUpdAsync");

                    }
                }
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

                //if (userModel == null)
                //    string Error = "Credenciales incorrectas o cuenta no registrada.";
            }
            catch (Exception e)
            {
                CapturarError(e);
               // userModel.Error = e.Message;
            }
           

            return userModel;

        }
        public async Task<AspNetUsers> ValidarUsuarioOld(AspNetUsers login)
        {
            AspNetUsers user = new AspNetUsers();

            string _query = @"SELECT top 1 a.Id, ISNULL(a.Email,''),ISNULL(a.PhoneNumber,''),a.UserName,ISNULL(a.Nombres,''),ISNULL(a.ApellidoPaterno,''),
                            ISNULL(a.ApellidoMaterno,''),ISNULL(a.FechaNacimiento, '1900-01-01'),c.Name as Role
                            FROM AspNetUsers a
                            LEFT JOIN aspNetUserRoles b on a.id = b.Userid
                            LEFT JOIN aspNetRoles c on c.id = b.RoleId
                            WHERE a.UserName=@UserName
                            AND a.PasswordHash=@Clave
                            order by c.Name Asc ";
             
            using (SqlConnection con = new(connectionString))
            {
                //System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
                //System.Globalization.CultureInfo.CurrentCulture = new CultureInfo("es-ES");
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es");

                using (SqlCommand cmd = new())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = _query;

                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar).Value = login.UserName;
                    cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = login.PasswordHash;
                    try
                    {
                        con.Open();
                        var reader = await cmd.ExecuteReaderAsync();
                        //user.Error = "Credenciales incorrectas o cuenta no registrada.";

                        while (reader.Read())
                        {
                            if (reader["UserName"] != null && login.UserName != null)
                            {

                                user.Id = reader.GetString(0);
                                user.Email = reader.GetString(1);
                                user.PhoneNumber = reader.GetString(2);
                                user.UserName = reader.GetString(3);
                                user.Nombres = reader.GetString(4);
                                user.ApellidoPaterno = reader.GetString(5);
                                user.ApellidoMaterno = reader.GetString(6);
                                user.FechaNacimiento = reader.GetDateTime(7);
                                //user.Role = reader.GetString(8);
                                //user.Error = string.Empty;



                            }
                            else
                            {
                               // user.Error = "Credenciales incorrectas o cuenta no registrada.";
                            }
                        }

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "ValidarUsuario");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "ValidarUsuario");

                    }

                    con.Close();
                }
            }

            return user;

        }

        public async Task<AspNetUsers> ChangePassword(AspNetUsers user)
        {
            string _query = @"UPDATE AspNetUsers set PasswordHash=@Clave
                            WHERE UserName=@UserName d ";

            using (SqlConnection con = new(connectionString))
            {
                using (SqlCommand cmd = new())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = _query;

                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar).Value = user.UserName;
                    cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = user.PasswordHash;
                    try
                    {
                        con.Open();
                        await cmd.ExecuteNonQueryAsync();

                    }
                    catch (SqlException ex)
                    {
                        CapturarError(ex, "Repository", "ChangePassword");

                    }
                    catch (Exception ex)
                    {
                        CapturarError(ex, "Repository", "ChangePassword");

                    }


                }
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
