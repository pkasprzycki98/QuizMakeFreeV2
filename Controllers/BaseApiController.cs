using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFree.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMakeFree.Controllers
{
	[Route("api/[controller]")]
	public class BaseApiController : Controller
	{
		#region konstruktor

		public BaseApiController(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;

			JsonSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented };
		}


		#endregion
		#region właściwości protected

		protected ApplicationDbContext DbContext { get; private set; }
		protected JsonSerializerSettings JsonSettings { get; private set; }
		#endregion

	}
}
