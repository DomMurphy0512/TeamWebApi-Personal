using System;

namespace TeamWebAPI
{
    public class Hobby
    {
        public int Id { get; set; }
        public string HobbyName { get; set; }
        public int HoursPerWeek { get; set; }
        public string Description { get; set; }
        public DateTime Started { get; set; }
    }
}
