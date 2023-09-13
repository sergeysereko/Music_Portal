using Microsoft.AspNetCore.Mvc;

namespace Music_Portal.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

    }
}
