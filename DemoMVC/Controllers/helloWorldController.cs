namespace PTPMQLQuangHung;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace DemoMVC.Controllers {
    public class helloWorldController : Controllers
    {
        //GET:/HelloWorld/
        public string Index()
        {
            return "this is my default action...";
        }

        //GET:/HelloWorld/Welcome/
        public string Welcome()
        {
            return "this is the Welcome action method";
        }
    }

    
}
