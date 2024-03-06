using Fast_Food.DAL.Interface;
using Fast_Food.DAL.Models;
using Fast_Food.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Fast_Food.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Get All data
        public async Task<IActionResult> Index()
        {
            TempData["RepositoryName"] = _employeeRepository.GetType().Name;

            var empList = await _employeeRepository.GetAllAsync();

            return View(empList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee emp, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte[] imageData = null;
                    if (image != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await image.CopyToAsync(memoryStream);
                            imageData = memoryStream.ToArray();
                            emp.Image = imageData;
                        }
                    }

                    var employee = new Employee
                    {
                        FName = emp.FName,
                        LName = emp.LName,
                        Telephone = emp.Telephone,
                        Job = emp.Job,
                        Age = emp.Age,
                        Salary = emp.Salary,
                        HireDate = emp.HireDate,
                        Image = emp.Image,
                        FullTime = emp.FullTime
                    };

                    int id = await _employeeRepository.Create(employee);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(emp);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee emp, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = await _employeeRepository.GetByIdAsync(id);

                    if (employee == null)
                    {
                        return NotFound();
                    }

                    employee.FName = emp.FName;
                    employee.LName = emp.LName;
                    employee.Telephone = emp.Telephone;
                    employee.Job = emp.Job;
                    employee.Age = emp.Age;
                    employee.Salary = emp.Salary;
                    employee.HireDate = emp.HireDate;
                    employee.FullTime = emp.FullTime;

                    if (image != null && image.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await image.CopyToAsync(memoryStream);
                            employee.Image = memoryStream.ToArray();
                        }
                    }

                    await _employeeRepository.Update(employee);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(emp);
        }

        public  IActionResult Delete(int id)
        {
            try
            {
                 _employeeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["DeleteErrors"] = ex.Message;
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
