using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;



namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbcontext _context;
        public PersonController(ApplicationDbcontext context) {
            
        }
        public async Task<IActionResult> Index()
        public IActionResult Create()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId", TypeFullNameComparer, AddressFamily")] Person person )
        public async Task 
        }
}
