using Microsoft.AspNetCore.Mvc;

namespace PersonalSite2.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View("Contact");
        }        
    }
}
