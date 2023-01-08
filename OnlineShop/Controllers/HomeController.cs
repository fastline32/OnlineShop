using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OnlineShop.Controllers
{
    public class HomeController : BaseApiController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger,IEmailService emailService)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet("test")]
        public async Task <ActionResult> Index()
        {
            UserEmailOptions userEmailOptions = new UserEmailOptions
            {
                ToEmails = new List<string>() {"pi@thesavov.net"}
            };

            await _emailService.SendTestEmail(userEmailOptions);
            return Ok();
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return Ok();
        }
    }
}