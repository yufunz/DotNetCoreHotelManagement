using Microsoft.AspNetCore.Mvc;

namespace IT703_A2.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
