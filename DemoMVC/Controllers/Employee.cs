using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using MvcMovie.Models;
using DemoMVC.Models;
namespace MvcMovie.Controllers
{
    public class EmployeeController : Controller
    {
       [HttpGet]
        public IActionResult Index() // 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Employee employee)
        {
            ViewBag.EmployeeId = employee.EmployeeId;
            ViewBag.FullName = employee.FullName;
            ViewBag.Age = employee.Age;
            return View();
        }
    }
}