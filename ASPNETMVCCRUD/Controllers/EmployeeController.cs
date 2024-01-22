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

            return RedirectToAction("AllEmployees");
        }


        [HttpGet]
        public async Task<IActionResult> AllEmployees()
        {
            var employees = await demoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await demoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewEmployee = new EditViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };

                return View(viewEmployee);
            }

            return  RedirectToAction("AllEmployees");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var employee = await demoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await demoDbContext.SaveChangesAsync();

                return RedirectToAction("AllEmployees");
            }
            return RedirectToAction("AllEmployees");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditViewModel model)
        {
            var employee = await demoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                demoDbContext.Remove(employee);
                await demoDbContext.SaveChangesAsync();

                return RedirectToAction("AllEmployees");
            }
            return RedirectToAction("AllEmployees");
        }
    }
}
