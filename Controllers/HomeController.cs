﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SignalR_App.Context;
using SignalR_App.Models;
using SignalR_App.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalR_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context= context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User userDto)
        {
            var user=_context.Users.SingleOrDefaultAsync(u=>u.Username == userDto.Username && u.Password==userDto.Password);
            if (user!=null) 
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userDto.Username),
                    new Claim(ClaimTypes.NameIdentifier,userDto.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var properties = new AuthenticationProperties
                {
                    RedirectUri = Url.Content("/support")
                };
                return SignIn(new ClaimsPrincipal(identity), properties, CookieAuthenticationDefaults.AuthenticationScheme);
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
