using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models; 

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
         public IActionResult Index(Person ps)
        {
            string strOutput = "Xin chào " + ps.PersonId + " - " + ps.FullName + " - " + ps.Address;
            ViewBag.infoPerson = strOutput;
            return View();
        }
    }
}
