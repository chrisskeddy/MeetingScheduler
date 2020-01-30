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
  public class HomeController : Controller
  {
    private MeetingSchedulerContext _context;

    //private readonly ILogger<HomeController> _logger;
    public HomeController(MeetingSchedulerContext context)
    {
      _context = context;
    }
    /*
    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }
    */

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult SignIn()
    {
      String email = Request.Form["email"].ToString();
      var exists = (from c in _context.Users where c.Id == email select c).FirstOrDefault();
      if (exists == null)
      {
        Users user = new Users
        {
          Id = email
        };
        _context.Users.Add(user);
      }

      DateTime dateTime = DateTime.Now.Add(new TimeSpan(0, 1, 0, 0, 0));
      String key = TokenGenerator.Generate(50);
      Usersigninkeys userSigninKey = new Usersigninkeys
      {
        Userid = email,
        Signinkey = key,
        Expire = dateTime
      };
      _context.Usersigninkeys.Add(userSigninKey);
      _context.SaveChanges();
      ViewData["email"] = email;
      ViewData["key"] = key;
      return View();
    }

    [HttpGet]
    public IActionResult SignInKey(string email, string key)
    {
      var validKey = (from c in _context.Usersigninkeys where c.Userid == email && c.Signinkey == key select c).FirstOrDefault();
      ViewData["invalid"] = false;
      ViewData["hasName"] = false;
      if (validKey == null)
      {
        ViewData["invalid"] = true;
      }
      else
      {
        DateTime dateTime = DateTime.Now;
        if (validKey.Expire < dateTime)
        {
          ViewData["invalid"] = true;
        }
        else
        {
          HttpContext.Session.SetString("email", email);
          String fullname = (from c in _context.Users where c.Id == email select c.Fullname).FirstOrDefault();
          if (fullname != null)
          {
            ViewData["hasName"] = true;
          }
        }
        _context.Usersigninkeys.Remove(validKey);
        _context.SaveChanges();
      }
      return View();
    }


    public IActionResult SetFullname(string Fullname)
    {
      if (HttpContext.Session.GetString("email") != null)
      {
        var email = HttpContext.Session.GetString("email");
        if (Fullname != null && Fullname.Length > 2)
        {
          var user = (from c in _context.Users where c.Id == email select c).FirstOrDefault();
          if (user != null)
          {
            user.Fullname = Fullname;
            _context.SaveChanges();
          }
        }
      }
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
