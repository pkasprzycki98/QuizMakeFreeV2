using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFreeWebApp.ViewModels;
using System.Collections.Generic;
using QuizMakeFree.Data;
using Mapster;
using QuizMakeFree.Data.Models;
using System.Linq;
using QuizMakeFree.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace QuizMakeFreeWebApp.Controllers
{
   [Route("api/[controller]")]
   public class ResultController : BaseApiController
   {
		

		#region kontruktor

		public ResultController(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(dbContext,roleManager,userManager,configuration)
		{
			
		}
		#endregion

		#region Metody dostosowujące do konwencji REST

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var results = DbContext.Results.Where(i => i.Id == id).FirstOrDefault();

			if (results == null)
			{
				return NotFound(new
				{
					Error = string.Format("nie znaleziono wyniku o id {0}", id)
				});
			}

			return new JsonResult(results.Adapt<ResultViewModel>(), JsonSettings);
		}


		[HttpPost]
		[Authorize]
		public IActionResult Post([FromBody]ResultViewModel model)
		{
			if (model == null) return new StatusCodeResult(500);

			var result = model.Adapt<Result>();


			result.CreatedDate = DateTime.Now;
			result.LastModifiedDate = result.CreatedDate;

			DbContext.Results.Add(result);

			DbContext.SaveChanges();

			return new JsonResult(result.Adapt<ResultViewModel>(), JsonSettings);

		}


		[HttpPut]
		[Authorize]
		public IActionResult Put([FromBody]ResultViewModel model)
		{
			if (model == null)
			{
				return new StatusCodeResult(500);
			}

			var result = DbContext.Results.Where(q => q.Id == model.Id).FirstOrDefault();

			if (result == null)
			{
				return NotFound(new
				{
					Error = string.Format("nie znaleziono wyniku o id {0}", model.Id)
				});
			}

			result.QuizId = model.QuizId;
			result.Text = model.Text;
			result.Notes = model.Notes;
			result.MinValue = model.MinValue;
			result.MaxValue = model.MaxValue;
			result.LastModifiedDate = DateTime.Now;

			DbContext.SaveChanges();

			return new JsonResult(result.Adapt<ResultViewModel>(), JsonSettings);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public IActionResult Delete(int id)
		{
			var result = DbContext.Results.Where(q => q.Id == id).FirstOrDefault();

			if (result == null)
			{
				return NotFound(new
				{
					Error = string.Format("Nie znaleziono wyniku o id {0}", id)
				});
			}

			DbContext.Results.Remove(result);

			DbContext.SaveChanges();

			return new NoContentResult();
		}

		#endregion

		[HttpGet("All/{quizId}")]
		public IActionResult All(int quizId)
		{
			var result = DbContext.Results.Where(q => q.QuizId == quizId).ToArray();

			return new JsonResult(result.Adapt<ResultViewModel[]>(),JsonSettings);
		}
	}
}

