using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFree.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using QuizMakeFree.Data.Models;

namespace QuizMakeFree.Controllers
{
	[Route("api/[controller]")]
	public class BaseApiController : Controller
	{
		#region konstruktor

		public BaseApiController(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			DbContext = dbContext;
			RoleManager = roleManager;
			UserManager = userManager;
			Configuration = configuration;
			JsonSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented };
		}


		#endregion
		#region właściwości protected

		protected ApplicationDbContext DbContext { get; private set; }
		protected RoleManager<IdentityRole> RoleManager { get; private set; }
		protected UserManager<ApplicationUser> UserManager { get; private set; }
		protected IConfiguration Configuration { get; private set; }
		protected JsonSerializerSettings JsonSettings { get; private set; }
		#endregion

	}
}
