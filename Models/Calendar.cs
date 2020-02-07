using System;
using System.Collections.Generic;

namespace MeetingScheduler.Models
{
    public partial class Calendar
    {
        public Calendar()
        {
            Availabletimes = new HashSet<Availabletimes>();
            Calendaraccess = new HashSet<Calendaraccess>();
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public string Userid { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Availabletimes> Availabletimes { get; set; }
        public virtual ICollection<Calendaraccess> Calendaraccess { get; set; }
    }
}
