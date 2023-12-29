using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Data;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly AppDbContext _context;

		public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,AppDbContext context)
		{
			_context = context;		
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult>Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid) return View(loginViewModel);

			var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

			if (user != null)
			{
				//User is found, check password
				var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
				if (passwordCheck)
				{
					//Password correct, sign in
					var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Race");
					}
				}
				//Password is incorrect
				TempData["Error"] = "Wrong credentials. Please try again";
				return View(loginViewModel);
			}
			//User not found
			TempData["Error"] = "Wrong credentials. Please try again";
			return View(loginViewModel);
		}
	}

}
