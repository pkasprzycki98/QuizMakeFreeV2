using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFreeWebApp.ViewModels;
using System.Collections.Generic;
using Mapster;
using QuizMakeFree.Data;
using System.Linq;
using QuizMakeFree.Data.Models;
using QuizMakeFree.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace QuizMakeFreeWebApp.Controllers
{
   [Route("api/[controller]")]
   public class AnswerController : BaseApiController
   {
		

		#region Kontruktor

		public AnswerController(ApplicationDbContext dbcontext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(dbcontext,roleManager,userManager,configuration)
		{
			
		}


		#endregion

		#region Metody dostosowujące do konwencji REST

		[HttpGet("{id}")]
      public IActionResult Get(int id)
      {
			var answer = DbContext.Answers.Where(a => a.Id == id).FirstOrDefault();

			if (answer == null)
			{
				return NotFound(new
				{
					Error = string.Format("nie znaleziono pytania o id {0}", id)
				});
			}

			return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);

      }

		[HttpPost]
		[Authorize]
		public IActionResult Post([FromBody]AnswerViewModel model)
		{
			if (model == null)
			{
				return new StatusCodeResult(500);
			}

			var answer = model.Adapt<Answer>();

			answer.CreatedDate = DateTime.Now;
			answer.LastModifiedDate = answer.CreatedDate;

			DbContext.Answers.Add(answer);

			DbContext.SaveChanges();

			return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);

		}


		[HttpPut]
		[Authorize]
		public IActionResult Put([FromBody]AnswerViewModel model)
      {
			if (model == null)
				return new StatusCodeResult(500);

			var answer = DbContext.Answers.Where(a => a.Id == model.Id).FirstOrDefault();


			if (answer == null)
			{
				return NotFound(new
				{
					Error = string.Format("nie znaleziono odpowiedzi o id {0}", model.Id)
				});
			}

			answer.QuestionId = model.QuestionId;
			answer.Text = model.Text;
			answer.Value = model.Value;
			answer.Notes = model.Notes;

			answer.LastModifiedDate = DateTime.Now;

			DbContext.SaveChanges();

			return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);



      }

      
      
      
      [HttpDelete("{id}")]
		[Authorize]
		public IActionResult Delete(int id)
      {
			var answer = DbContext.Answers.Where(a => a.Id == id).FirstOrDefault();

			if (answer == null)
			{
				return NotFound(new
				{
					Error = string.Format("Nie znaleziono odpowiedzi o id {0}", id)
				});
			}


			DbContext.Answers.Remove(answer);
			DbContext.SaveChanges();

			return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);
		}
      #endregion

      // GET api/answer/all
      [HttpGet("All/{questionId}")]
      public IActionResult All(int questionId)
      {
			var answers = DbContext.Answers.Where(a => a.QuestionId == questionId).ToArray();

         // Przekaż wyniki w formacie JSON
         return new JsonResult(answers.Adapt<AnswerViewModel[]>(),JsonSettings);
      }
   }
}

