﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MeetingScheduler.Models;

namespace MeetingScheduler.Controllers
{
  public class HomeController : Controller
  {
    private MeetingPlannerContext _context;
    private readonly ILogger<HomeController> _logger;


    public HomeController(MeetingPlannerContext context)
    {
      _context = context;
    }
    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult SignIn()
    {
      ViewData["email"] = Request.Form["email"].ToString();
      UserSigninKeys userSigninKeys = new UserSigninKeys
      {
        UserId = ViewData["email"].ToString(),
        SigninKey = "BLARANDOMKEY12345"
      };

      return View();
    }

    public IActionResult Session()
    {
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
