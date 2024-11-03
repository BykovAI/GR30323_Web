using Microsoft.AspNetCore.Mvc;

namespace GR30323_Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
