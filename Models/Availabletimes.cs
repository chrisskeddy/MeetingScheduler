using System;
using System.Collections.Generic;

namespace MeetingScheduler.Models
{
    public partial class Availabletimes
    {
        public long Id { get; set; }
        public string Calendarid { get; set; }
        public DateTime Editstamp { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }

        public virtual Calendar Calendar { get; set; }
    }
}
