using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using DemoMVC.Data;

using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using DemoMVC;
using MvcMovie.Models.Process;
using System.Data;


namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _context.Person.ToListAsync();
            return View(model);
        }


        public IActionResult Create()
        {
            string newId = Add.IdGenerator.GenerateId(_context.Person, "PS", "PersonId");
            ViewBag.NewPersonId = newId;


            return View();
        }


        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Upload(IFormFile file)// cơ chế xử lý bất đồng bộ 
{
    if (file != null)
    {
        string fileExtension = Path.GetExtension(file.FileName);
        if (fileExtension != ".xls" && fileExtension != ".xlsx")
        {
            ModelState.AddModelError("", "Please choose an Excel file to upload!");
        }
        else
        {
            var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Excels", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Gọi ExcelProcess để đọc dữ liệu từ file Excel
            DataTable dt = _excelProcess.ExcelToDataTable(filePath);

            // Duyệt từng dòng trong DataTable và thêm vào DbContext
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                        var person = new Person
                        {
                            PersonId = dt.Rows[i][0].ToString(),
                            FullName = dt.Rows[i][1].ToString(),
                            Address = dt.Rows[i][2].ToString(),

                        };

                // Kiểm tra nếu chưa tồn tại thì mới thêm
                if (!_context.Person.Any(p => p.PersonId == person.PersonId))
                {
                    _context.Person.Add(person);
                }
            }

            await _context.SaveChangesAsync();
            ViewBag.Message = "Upload and import successful!";
        }
    }
    else
    {
        ModelState.AddModelError("", "Please upload a file.");
    }

    return View();
}



        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address,Email")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Person' is null.");
            }

            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
