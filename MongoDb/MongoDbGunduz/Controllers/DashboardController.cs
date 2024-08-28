using Microsoft.AspNetCore.Mvc;

namespace MongoDbGunduz.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
