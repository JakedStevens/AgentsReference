using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Agents.Models;
using Agents.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Agents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AgentsData _agentsData;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, AgentsData agentsData)
        {
            _logger = logger;
            _configuration = configuration;
            _agentsData = agentsData;
        }

        public IActionResult Index()
        {
            var agents = _agentsData.AllAgentData();

            var vm = new HomeViewModel();
            vm.Agents = agents;

            return View(vm);
        }

        public IActionResult AgentData()
        {
            var agents = _agentsData.AllAgentData();

            return Json(agents);
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
