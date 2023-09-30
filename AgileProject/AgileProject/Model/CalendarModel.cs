﻿using System.ComponentModel.DataAnnotations;

namespace AgileProject.Model
{
    public class CalendarModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? AllDay { get; set; }
        public string? Color { get; set; }
        public int? EventTypeId { get; set; }
        public string? EventTypeName { get; set; }
        public int? CalendarTypeId { get; set; }
        public string? CalendarTypeName { get; set; }
        public string? Description { get; set; }
        public string? UserCreate { get; set; }
        public DateTime? DateCreate { get; set; }
        public string? UserName { get; set; }

        public List<EventTypeModel>? EvenTypeList { get; set; }
        public List<CalendarModel>? CalendarList { get; set; }

    }
}

