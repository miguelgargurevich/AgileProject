namespace AgileProject.Entidades
{
    public class Calendar
    {
            public int id { get; set; }
            public string? title { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public bool? allDay { get; set; }
            public string? color { get; set; }
            public int? EventTypeId { get; set; }
            public string? type { get; set; }
            public int CalendarTypeId { get; set; }
            public string? CalendarTypeName { get; set; }
            public string? description { get; set; }
            public string? UserCreate { get; set; }
            public DateTime? DateCreate { get; set; }
            public string? UserName { get; set; }

        public static implicit operator List<object>(Calendar? v)
        {
            throw new NotImplementedException();
        }
    }
}
