using System;
using System.Collections.Generic;

namespace MeetingScheduler.Models
{
  public partial class Calendaraccess
  {
    public long Calendarid { get; set; }
    public string Userid { get; set; }
    public DateTime? Expire { get; set; }
    public int Meetingcount { get; set; }
    public int Meetingminutelength { get; set; }

    public virtual Calendar Calendar { get; set; }
    public virtual Users User { get; set; }
  }
}
