using System;

namespace MeetingScheduler.Models
{
  public class UserSigninKeys
  {
    public string UserId { get; set; }
    public string SigninKey { get; set; }
    public DateTime Expire { get; set; }
  }
}