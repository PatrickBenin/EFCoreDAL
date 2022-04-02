using EFCoreDAL;
using Microsoft.AspNetCore.Mvc;
using EmpDeptApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmpDeptApp.Controllers
{
    public class EmployeeController : Controller
    {
        AppDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeController(AppDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            //var emps = _db.Employees.Select(p => p).ToList();

            List<EmpDeptViewModel> EmpDeptViewModellist = new List<EmpDeptViewModel>(); 

            var empsList = (from e in _db.Employees
                        join d in _db.Departments on e.DeptId equals d.DeptId
                        select new
                        {
                            e.EmpId,
                            e.Name,
                            e.Address,
                            e.ImagePath,
                            d.DeptId,
                            d.DeptName
                        }).ToList();

            //query getting data from database from joining two tables and storing data in employeelist

            foreach (var item in empsList)

            {
                EmpDeptViewModel objedvm = new EmpDeptViewModel(); // ViewModel

                objedvm.Name = item.Name;
                objedvm.DeptName = item.DeptName;
                objedvm.Address = item.Address;
                objedvm.EmpId = item.EmpId;
                objedvm.ImagePath = item.ImagePath;
                EmpDeptViewModellist.Add(objedvm);
            }

            //Using foreach loop fill data from custmerlist to List<EmployeeVM>.

            return View(EmpDeptViewModellist); //List of EmployeeVM (ViewModel)
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            ModelState.Remove("EmpId");
            //if (ModelState.IsValid)
            //{
            //    _db.Employees.Add(model);
            //    _db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View();

            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Employee employee = new Employee
                {
                    Name = model.Name,
                    Address = model.Address,
                    DeptId = model.DeptId,
                    ImagePath = uniqueFileName,
                };
                _db.Add(employee);
                 _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        private string UploadedFile(EmployeeViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageEmp != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageEmp.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageEmp.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Edit(int id)
        {
            Employee model = _db.Employees.Find(id);
            //ViewBag.Departments = _db.Departments.ToList();

            ViewData["DeptId"] = new SelectList(_db.Departments, "DeptId", "Name",model.DeptId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Update(model);                
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _db.Departments.ToList();
            return View();
        }

        public IActionResult Delete(int id)
        {
            Employee model = _db.Employees.Find(id);
            if (model != null)
            {
                _db.Employees.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }




    }
}
