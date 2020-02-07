using System;
using System.Collections.Generic;

namespace MeetingScheduler.Models
{
    public partial class Usersigninkeys
    {
        public string Userid { get; set; }
        public string Signinkey { get; set; }
        public DateTime Expire { get; set; }
        public string Code { get; set; }

        public virtual Users User { get; set; }
    }
}
