using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using v6.Models;

namespace v6.Controllers
{
    public class StudentController : Controller
    {
        private readonly Context  _context;

        public StudentController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentModel studentModel)
        {
            /*var std = new StudentModel()
            {
                Id = Guid.NewGuid(),
                Name = studentModel.Name,
                Address = studentModel.Address,
            };*/
/*            _context.Students.Add(std);
*/ 
            await _context.Students.AddAsync(studentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Add");
        }
        [HttpGet]
        public async Task<IActionResult> Display()
        {
            var students = await _context.Students.ToListAsync();  
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> ViewStudent(Guid id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> ViewStudent(StudentModel model)
        {
            var student = await _context.Students.FindAsync(model.Id);

           if (student != null)
            {
                student.Name = model.Name;
                student.Address = model.Address;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Display");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentModel model)
        {
            var student = await _context.Students.FindAsync(model.Id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Display");
            }
            return RedirectToAction("Display");

        }
    }
}
