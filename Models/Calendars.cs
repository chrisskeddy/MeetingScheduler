using System;
namespace MeetingScheduler.Models
{
  public class Calendars
  {
    public string SessionId { get; set; }
    public string UserId { get; set; }
    public string Calendar { get; set; }
    public DateTime Stamped { get; set; }
  }
}