using RecaptchaExample.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RecaptchaExample.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index(string name)
        {
            var captchaResponse = Request.Form["g-Recaptcha-Response"];
            var result = ReCaptchaValidator.IsValid(captchaResponse);

            if (!result.Success)
            {
                if (result.ErrorCodes == null)
                {
                    result.ErrorCodes = new List<string>() { "NOT_HUMAN" };
                }

                foreach (string err in result.ErrorCodes)
                {
                    ModelState.AddModelError("", err);
                }
            }

            result.Name = name;

            return View(result);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}