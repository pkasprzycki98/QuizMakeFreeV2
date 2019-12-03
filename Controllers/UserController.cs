using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuizMakeFree.Data;
using QuizMakeFree.Data.Models;
using QuizMakeFree.ViewModels;

namespace QuizMakeFree.Controllers
{
	public class UserController : BaseApiController
	{

		public UserController(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(dbContext, roleManager, userManager, configuration) {

		}
		#region  metordyRest
		
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]UserViewModel model)
		{
			// Dane od klienta niewłaściwe,zwraca Http 500 ( serwer error)
			if (model == null) return new StatusCodeResult(500);

			ApplicationUser user = await UserManager.FindByNameAsync(model.UserName);
			if (user != null) return BadRequest("Nazwa użytkownika jest już zajęta!");

			var now = DateTime.Now;

			user = new ApplicationUser()
			{

				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = model.UserName,
				Email = model.Email,
				DisplayName = model.DisplayName,
				CreatedDate = now,
				LastModifiedDate = now
			};

			user.EmailConfirmed = true;
			user.LockoutEnabled = false;

			string user_role = "User";

			await UserManager.CreateAsync(user, model.Password);

			 if(await RoleManager.RoleExistsAsync(user_role))
			await UserManager.AddToRoleAsync(user, user_role);

			DbContext.SaveChanges();

			return Json(user.Adapt<UserViewModel>(), JsonSettings);
		}
		#endregion
		public IActionResult Index()
        {
            return View();
        }
    }
}