using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    // made for the folder to be visible in git
    public class EmptyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
