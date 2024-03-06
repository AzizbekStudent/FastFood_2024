using Fast_Food.DAL.Interface;
using Fast_Food.DAL.Models;
using Fast_Food.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            TempData["RepositoryName"] = _employeeRepository?.GetType()?.Name;

            var entities = await _employeeRepository.GetAllAsync();

            if(entities == null)
            { 
                return NotFound(); 
            }

            return View(entities);
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
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeRepository.Create(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            return View(_employeeRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeRepository.Update(emp);
                    return RedirectToAction("Details", new { id = emp.Employee_ID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(emp);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _employeeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["DeleteErrors"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
