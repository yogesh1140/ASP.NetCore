using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Working.Models;
using Working.Services;
using Working.ViewModels;
namespace Working.Controllers.Web
{
    public class AppController: Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot  _config;
        private IWorkingRepository _repository;
        private ILogger<AppController> _logger;
        private HttpClient _client { get; set; }


        //private WorkingContext _context;

        public AppController(
            IMailService mailService, 
            IConfigurationRoot config, 
            IWorkingRepository repository, 
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5000/");
           // _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
        }   

        public IActionResult Index()
        {
            try
            {
              //  var data = _repository.GetTripsByUserName(this.User.Identity.Name);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get trips in Index page: { ex.Message}");
                return Redirect("/error");
                
            }
           
        }
        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                var trips = _repository.GetTripsByUserName(this.User.Identity.Name);
                return View(trips);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get trips in Trips page: { ex.Message}");
                return Redirect("/error");

            }

        }
            public IActionResult Contact()
        {
           // throw new InvalidOperationException("Bad things happen to good developers");
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com")) ModelState.AddModelError("", "We dont support AOL address");
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], "from", "subject", "body");
                ViewBag.UserMessage = "Message Sent!";
                ModelState.Clear();
            }
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
