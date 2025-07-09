using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace DemoMvc.Controllers
{
    public class DemoPersonController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

     
        public string Person()
        {
            return "this is the Welcome action method";
        }
    }
}