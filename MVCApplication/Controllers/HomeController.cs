using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models;
using MVCApplication.Services;
using System.Diagnostics;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GoogleCaptchaService _captchaService;

        public HomeController(ILogger<HomeController> logger, GoogleCaptchaService googleCaptchaService)
        {
            _logger = logger;
            _captchaService = googleCaptchaService;
        }

        public IActionResult Index(ContactFormModel contactFormModel)
        {
            //var captchaResult = await _captchaService.VerifyToken(contactFormModel.Token);
            //if(!captchaResult)
            //{
            //    return View();
            //}
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Form(ContactFormModel contactFormModel)
        {
            var captchaResult = await _captchaService.VerifyToken(contactFormModel.Token);
            if(!captchaResult)
            {
                return View();
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
