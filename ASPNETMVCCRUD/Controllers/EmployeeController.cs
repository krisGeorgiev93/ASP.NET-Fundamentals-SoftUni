using Microsoft.AspNetCore.Mvc;

namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
