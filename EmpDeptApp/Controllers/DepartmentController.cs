using EFCoreDAL;
using Microsoft.AspNetCore.Mvc;

namespace EmpDeptApp.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext _db;

        public DepartmentController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var depts = _db.Departments.Select(p => p).ToList();
            return View(depts);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department model)
        {
            ModelState.Remove("DeptId");
            if (ModelState.IsValid)
            {
                _db.Departments.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public IActionResult Edit(int id)
        {
            Department model = _db.Departments.Find(id);
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View();
        }

        public IActionResult Delete(int id)
        {
            Department model = _db.Departments.Find(id);
            if (model != null)
            {
                _db.Departments.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
