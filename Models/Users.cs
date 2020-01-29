using System;
using System.Collections.Generic;

namespace MeetingScheduler.Models
{
    public partial class Users
    {
        public Users()
        {
            Calendar = new HashSet<Calendar>();
            Calendaraccess = new HashSet<Calendaraccess>();
            MeetingsRequestuser = new HashSet<Meetings>();
            MeetingsUser = new HashSet<Meetings>();
            Usersigninkeys = new HashSet<Usersigninkeys>();
        }

        public string Id { get; set; }
        public string Fullname { get; set; }

        public virtual ICollection<Calendar> Calendar { get; set; }
        public virtual ICollection<Calendaraccess> Calendaraccess { get; set; }
        public virtual ICollection<Meetings> MeetingsRequestuser { get; set; }
        public virtual ICollection<Meetings> MeetingsUser { get; set; }
        public virtual ICollection<Usersigninkeys> Usersigninkeys { get; set; }
    }
}
