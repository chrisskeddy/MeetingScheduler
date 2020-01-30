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
      var Email = HttpContext.Session.GetString("Email");
      if (Email == null)
      {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
      }
      ViewData["Email"] = Email;
      ViewData["Fullname"] = HttpContext.Session.GetString("Fullname");
      return View();
    }
  }
}