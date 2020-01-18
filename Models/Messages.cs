using System;

namespace MeetingScheduler.Models
{
  public class Messages
  {
    public string UserId { get; set; }
    public string Message { get; set; }
    public string SessionId { get; set; }
    public DateTime Stamped { get; set; }
  }
}