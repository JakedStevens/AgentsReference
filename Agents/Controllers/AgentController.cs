using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Agents.Controllers
{
    public class AgentController : Controller
    {
        private readonly ILogger<AgentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SingleAgent _singleAgent;

        public AgentController(ILogger<AgentController> logger, IConfiguration configuration, SingleAgent singleAgent)
        {
            _logger = logger;
            _configuration = configuration;
            _singleAgent = singleAgent;
        }

        public IActionResult Detail(string id)
        {
            var agent = _singleAgent.GetSingleAgentData(id);

            return View(agent);
        }
    }
}
