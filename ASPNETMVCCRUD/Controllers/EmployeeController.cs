using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DemoDbContext demoDbContext;

        public EmployeeController(DemoDbContext demoDbContext)
        {
            this.demoDbContext = demoDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel empl)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = empl.Name,
                Email = empl.Email,
                Salary = empl.Salary,
                Department = empl.Department,
                DateOfBirth = empl.DateOfBirth,
            };

           await demoDbContext.Employees.AddAsync(employee);
           await demoDbContext.SaveChangesAsync();

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> AllEmployees()
        {
            var employees = await demoDbContext.Employees.ToListAsync();
            return View(employees);
        }
    }
}
