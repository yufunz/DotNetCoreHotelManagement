﻿using IT703_A2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using IT703_A2.Services;

namespace IT703_A2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public IActionResult All()
        {
            var currentDashboard = this.homeService.GetDashboardInfo();

            return View(currentDashboard);
        }

    }
}