using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFreeWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using QuizMakeFree.Data;
using Mapster;
using QuizMakeFree.Data.Models;
using QuizMakeFree.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace QuizMakeFreeWebApp.Controllers
{
   [Route("api/[controller]")]
   public class QuestionController : BaseApiController
   {
		

		#region kontruktor

		public QuestionController(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(dbContext,roleManager,userManager,configuration)
		{		
		}
		#endregion

		#region Metody dostosowujące do konwencji REST

		[HttpGet("{id}")]
      public IActionResult Get(int id)
      {
			var question = DbContext.Questions.Where(i => i.Id == id).FirstOrDefault();

			if (question == null)
			{
				return NotFound(new {
					Error = string.Format("nie znaleziono pytania o id {0}", id)
				});
			}

			return new JsonResult(question.Adapt<QuestionViewModel>(), JsonSettings);
      }

      
      [HttpPost]
      public IActionResult Post([FromBody]QuestionViewModel model)
      {
			if (model == null) return new StatusCodeResult(500);

			var question = model.Adapt<Question>();


			question.CreatedDate = DateTime.Now;
			question.LastModifiedDate = question.CreatedDate;

			DbContext.Questions.Add(question);

			DbContext.SaveChanges();

			return new JsonResult(question.Adapt<QuestionViewModel>(), JsonSettings);

      }

     
      [HttpPut]
      public IActionResult Put([FromBody]QuestionViewModel model)
      {
			if (model == null)
			{
				return new StatusCodeResult(500);
			}

			var question = DbContext.Questions.Where(q => q.Id == model.Id).FirstOrDefault();

			if (question == null)
			{
				return NotFound(new
				{
					Error = string.Format("nie znaleziono pytania o id {0}", model.Id)
				});
			}

			question.QuizId = model.QuizId;
			question.Text = model.Text;
			question.Notes = model.Notes;
			question.LastModifiedDate = DateTime.Now;

			DbContext.SaveChanges();

			return new JsonResult(question.Adapt<QuestionViewModel>(), JsonSettings);
      }

      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
			var question = DbContext.Questions.Where(q => q.Id == id).FirstOrDefault();

			if (question == null)
			{
				return NotFound(new
				{
					Error = string.Format("Nie znaleziono pytania o id {0}", id)
				});
			}

			DbContext.Questions.Remove(question);

			DbContext.SaveChanges();

			return new NoContentResult();
      }

      #endregion

      [HttpGet("All/{quizId}")]
      public IActionResult All(int quizId)
      {
			var question = DbContext.Questions.Where(q => q.QuizId == quizId).ToArray();
        
         return new JsonResult(question.Adapt<QuestionViewModel[]>(),JsonSettings);
      }
   }
}

