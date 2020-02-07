using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MeetingScheduler.Models;

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