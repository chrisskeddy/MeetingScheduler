using System;
namespace MeetingScheduler.Models
{
  public class Days
  {
    public long Id { get; set; }
    public string SessionId { get; set; }
    public string UserId { get; set; }
    public DateTime Stamped { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
  }
}