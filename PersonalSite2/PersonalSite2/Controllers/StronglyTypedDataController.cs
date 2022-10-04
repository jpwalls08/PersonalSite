using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonalSite2.Models;


namespace PersonalSite2.Controllers
{
    public class StronglyTypedDataController : Controller
    {
        //Add a field for the Configuration settings in appsettings.json
        private readonly IConfiguration _config;

        //Add a constructor for our controller that accepts the config info as a parameter
        public StronglyTypedDataController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
