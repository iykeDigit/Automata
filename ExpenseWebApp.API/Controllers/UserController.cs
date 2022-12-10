using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseWebApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseWebApp.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("You can get users ");
            var name = "My name is Abimbola Olaitan";
            return Ok(name);
        }
    }
}
