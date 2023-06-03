using Microsoft.AspNetCore.Mvc;

namespace PROJETO_SMIMA.Controllers
{
    public class DonateController : Controller
    {
        public IActionResult Donate()
        {
            return View();
        }
    }
}
