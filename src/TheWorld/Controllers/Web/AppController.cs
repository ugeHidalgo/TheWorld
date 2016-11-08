
using Microsoft.AspNetCore.Mvc;
using Microsoft.Framework.Configuration;
using System.Linq;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var data = _repository.GetAllTrips();
            return View(data);  //renderiza la vista y la devuelve, se le pasan los datos de los trips.
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "Cannot send emails to aol.com");
                //Si dejamos Email vacio se aplica a todos los campos y se muestra en el summary
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, model.Name, model.Message);
                ViewBag.UserMessage = "Message sent !!!";
                ModelState.Clear(); //Borra el form
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
