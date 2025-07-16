using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using DemoMVC.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using DemoMVC;
using DemoMVC.Models.Process;
using System.Data;
using OfficeOpenXml;


namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public string? PersonId { get; private set; }
        public string? FullName { get; private set; }
        public string? Address { get; private set; }

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

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
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
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Excels");
Directory.CreateDirectory(folderPath);
                    var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(folderPath, fileName);
                    
                     using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);


                        // Gọi ExcelProcess để đọc dữ liệu từ file Excel
                        var dt = _excelProcess.ExcelToDataTable(filePath);

                        // Duyệt từng dòng trong DataTable và thêm vào DbContext
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var ps = new Person();

                            ps.PersonId = dt.Rows[i][0].ToString();
                            ps.FullName = dt.Rows[i][1].ToString();
                            ps.Address = dt.Rows[i][2].ToString();

                            _context.Add(ps);



                            // Kiểm tra nếu chưa tồn tại thì mới thêm
                        }

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }

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


 public IActionResult Download()
{
    // Name the file when downloading
    var fileName = "YourFileName" + ".xlsx";
    using (ExcelPackage excelPackage = new ExcelPackage())
    {
        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

        // add some text to cell A1
        worksheet.Cells["A1"].Value = "PersonID";
        worksheet.Cells["B1"].Value = "FullName";
        worksheet.Cells["C1"].Value = "Address";

        // get all Person
        var personList = _context.Person.ToList();

        // fill data to worksheet
        worksheet.Cells["A2"].LoadFromCollection(personList);

        var stream = new MemoryStream(excelPackage.GetAsByteArray());

        // download file
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
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
