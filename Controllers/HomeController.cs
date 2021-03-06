﻿using System;
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

    public IActionResult SignInEmail()
    {
      if (HttpContext.Session.GetString("email") != null)
      {
        return RedirectToAction("Index", "Calendar");
      }
      return View();
    }
    public IActionResult Index()
    {
      if (HttpContext.Session.GetString("email") != null)
      {
        return RedirectToAction("Index", "Calendar");
      }
      return View();
    }

    [HttpPost]
    public IActionResult SignIn(string email)
    {
      if (HttpContext.Session.GetString("email") != null)
      {
        return RedirectToAction("Index", "Calendar");
      }
      if (email == null || email == "")
      {
        return RedirectToAction("SignInEmail", "Home");
      }
      var exists = (from c in _context.Users where c.Id == email select c).FirstOrDefault();
      if (exists == null)
      {
        Users user = new Users
        {
          Id = email
        };
        _context.Users.Add(user);
      }

      DateTime expireTime = DateTime.Now.Add(new TimeSpan(0, 1, 0, 0, 0));
      String key = TokenGenerator.Generate(50, false);
      String code = TokenGenerator.Generate(6, true);
      Usersigninkeys userSigninKey = new Usersigninkeys
      {
        Userid = email,
        Signinkey = key,
        Expire = expireTime,
        Code = code
      };
      _context.Usersigninkeys.Add(userSigninKey);
      _context.SaveChanges();
      ViewData["email"] = email;
      ViewData["key"] = key;
      ViewData["code"] = code;
      return View();
    }

    [HttpPost]
    public IActionResult SignInKey(string email, string key, string code)
    {
      if (code == null || code == "")
      {
        return RedirectToAction("SignInEmail", "Home");
      }
      var upperCode = code.ToUpper().Replace(" ", string.Empty);
      var ValidKey = (from c in _context.Usersigninkeys where upperCode == c.Code && c.Userid == email && c.Signinkey == key select c).FirstOrDefault();
      ViewData["invalid"] = false;
      ViewData["hasName"] = false;
      if (ValidKey == null)
      {
        ViewData["invalid"] = true;
      }
      else
      {
        DateTime dateTime = DateTime.Now;
        if (ValidKey.Expire < dateTime)
        {
          ViewData["invalid"] = true;
        }
        else
        {
          //HttpResponse.Cookies(".AspNetCore.Session").Expires = DateTime.Now.AddMinutes(20);
          HttpContext.Session.SetString("email", email);
          String fullname = (from c in _context.Users where c.Id == email select c.Fullname).FirstOrDefault();
          if (fullname != null)
          {
            HttpContext.Session.SetString("fullname", fullname);
            return RedirectToAction("Index", "Calendar");
          }
        }
        _context.Usersigninkeys.Remove(ValidKey);
        _context.SaveChanges();
      }
      return View();
    }


    [HttpPost]
    public IActionResult SetFullname(string fullname)
    {
      if (HttpContext.Session.GetString("email") != null)
      {
        var Email = HttpContext.Session.GetString("email");
        if (fullname != null && fullname.Length > 2)
        {
          var user = (from c in _context.Users where c.Id == Email select c).FirstOrDefault();
          if (user != null)
          {
            user.Fullname = fullname;
            HttpContext.Session.SetString("fullname", fullname);

            _context.SaveChanges();
            return RedirectToAction("Index", "Calendar");
          }
        }
      }
      return RedirectToAction("Index");
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
