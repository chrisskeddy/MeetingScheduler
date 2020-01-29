using System;
using System.Collections.Generic;

namespace MeetingScheduler.Models
{
    public partial class Meetings
    {
        public long Id { get; set; }
        public string Userid { get; set; }
        public string Requestuserid { get; set; }
        public string Description { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }

        public virtual Users Requestuser { get; set; }
        public virtual Users User { get; set; }
    }
}
