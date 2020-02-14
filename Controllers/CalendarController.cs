using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MeetingScheduler.Models;
using System.Text.Json;

namespace MeetingScheduler.Controllers
{
  public class CalendarController : Controller
  {
    private MeetingSchedulerContext _context;

    //private readonly ILogger<HomeController> _logger;
    public CalendarController(MeetingSchedulerContext context)
    {
      _context = context;
    }

    public IActionResult CalendarAccessJSON()
    {
      var email = HttpContext.Session.GetString("email");
      if (email == null)
      {
        return Json(new { status = "invalid" });
      }
      var calendarAccess = (from c in _context.Calendaraccess where c.Userid == email && c.Expire <= DateTime.Now select c).ToArray();
      return Json(new
      {
        status = "success",
        data = calendarAccess.Select(
          item => new
          {
            userid = item.Userid,
            description = item.Calendarid,
            meetingcount = item.Meetingcount,
            meetingminutelength = item.Meetingminutelength
          }
        )
      });
    }

    public IActionResult MeetingsJSON()
    {
      var email = HttpContext.Session.GetString("email");
      if (email == null)
      {
        return Json(new { status = "invalid" });
      }
      var now = DateTime.Now;
      var oldMeetings = (from c in _context.Meetings where c.Starttime < now && c.Userid == email select c).ToArray();
      if (oldMeetings != null)
      {
        for (int i = 0; i < oldMeetings.Length; ++i)
        {
          _context.Meetings.Remove(oldMeetings[i]);
        }
        _context.SaveChanges();
      }
      var meetings = (from c in _context.Meetings where c.Starttime >= now && c.Userid == email select c).Take(15).ToArray();
      return Json(new
      {
        status = "success",
        data = meetings.Select(
          item => new
          {
            id = item.Id,
            description = item.Description,
            userid = email,
            starttime = item.Starttime.ToString("yyyy-MM-dd HH:mm:ss"),
            endtime = item.Endtime.ToString("yyyy-MM-dd HH:mm:ss"),
            requestuserid = item.Requestuserid
          }
        )
      });
    }
    public IActionResult CalendarsJSON()
    {
      var email = HttpContext.Session.GetString("email");
      if (email == null)
      {
        return Json(new { status = "invalid" });
      }
      var calendars = (from c in _context.Calendar where c.Userid == email select c).ToArray();

      //return JsonSerializer.Parse<WeatherForecast>(json, options);
      //var calendars = new 
      return Json(new
      {
        status = "success",
        data = calendars.Select(
        item => new
        {
          id = item.Id,
          description = item.Description,
          userid = email
        }
      )
      });
    }

    public IActionResult AvailableTimesJSON(long calendarId)
    {
      var email = HttpContext.Session.GetString("email");
      if (email == null)
      {
        return Json(new { status = "invalid" });
      }
      var calendar = (from c in _context.Calendar where c.Userid == email && c.Id == calendarId select c).FirstOrDefault();
      if (calendar == null)
      {
        return Json(new { status = "invalid" });
      }
      var availiableTimes = (from c in _context.Availabletimes where c.Calendarid == calendar.Id select c).ToArray();

      return Json(new
      {
        status = "success",
        data = availiableTimes.Select(
        item => new
        {
          id = item.Id,
          calendarid = item.Calendarid,
          starttime = item.Starttime.ToString("yyyy-MM-dd HH:mm:ss"),
          endtime = item.Endtime.ToString("yyyy-MM-dd HH:mm:ss")
        }
      )
      });
    }

    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Index", "Home");
    }

    public IActionResult Index()
    {
      var email = HttpContext.Session.GetString("email");
      if (email == null)
      {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
      }
      ViewData["email"] = email;
      ViewData["fullname"] = HttpContext.Session.GetString("fullname");
      return View();
    }

    public IActionResult Calendars()
    {
      return View();
    }
  }
}