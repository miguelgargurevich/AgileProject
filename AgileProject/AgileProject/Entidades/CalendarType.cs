using System;
//using Microsoft.AspNetCore.Identity;

namespace AgileProject.Entidades
{
    public class CalendarType 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? GroupType { get; set; }
        public bool IsChecked { get; set; }
    }
}

