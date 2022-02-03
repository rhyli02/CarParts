using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CartParts.Models;
using CartParts.Services.Interfaces;

namespace CartParts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProfileServices _services;
        public HomeController(ILogger<HomeController> logger, IProfileServices services)
        {
            _logger = logger;
            _services = services;
        }

        public async Task<IActionResult> IndexAsync()
        {

            List<Profile> res = (List<Profile>)await _services.GetAllProfiles();
            
            return View(res);
        }

        public IActionResult Privacy()
        {


            return View();
        }
        public async Task<IActionResult> DetailsAsync(int id)
        {
            Profile result =await _services.GetProfileById(id);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
