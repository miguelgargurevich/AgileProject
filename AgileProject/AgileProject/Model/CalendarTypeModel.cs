using System;
//using Microsoft.AspNetCore.Identity;

namespace AgileProject.Model
{
    public class CalendarTypeModel 
    {
        public int CustomId { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? GroupType { get; set; }
    }
}

