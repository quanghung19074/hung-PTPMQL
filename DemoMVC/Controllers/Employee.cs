using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

     
        public string Employee()
        {
            return "this is the Welcome action method";
        }
    }
}