
using Microsoft.AspNetCore.Mvc;
using Microsoft.Framework.Configuration;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();  //renderiza la vista y la devuelve.
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
