﻿using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFreeWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using QuizMakeFree.Data;
using Mapster;
using QuizMakeFree.Data.Models;
using Microsoft.Azure.KeyVault.Models;

namespace QuizMakeFreeWebApp.Controllers
{
   [Route("api/[controller]")]
   public class QuizController : Controller
   {

		#region pola prywatne

		private ApplicationDbContext DbContext;
		#endregion

		#region Konstuktor

		public QuizController(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;
		}
		#endregion

		#region Metody dostosowujące do konwencji REST
		
		[HttpGet("{id}")]
      public IActionResult Get(int id)
      {
			var quiz = DbContext.Quizzes.Where(i => i.Id == id).FirstOrDefault();

			if (quiz == null)
			{
				return NotFound(new
				{ Error = string.Format("nie znaleziono quizy o id: {0}", id) });
			}

		
			return new JsonResult(quiz.Adapt<QuizViewModel>(), new JsonSerializerSettings()
   			 {
				Formatting = Formatting.Indented
		 });
      }

      
      [HttpPost]
      public IActionResult Post([FromBody]QuizViewModel model)
      {
			if (model == null)
			{
				return new StatusCodeResult(500);
			}
			var quiz = new Quiz();

			quiz.Title = model.Title;
			quiz.Description = model.Description;
			quiz.Text = model.Text;
			quiz.Notes = model.Notes;


			quiz.CreatedDate = DateTime.Now;
			quiz.LastModifiedDate = quiz.CreatedDate;

			quiz.UserId = DbContext.Users.Where(u => u.UserName == "Admin").FirstOrDefault().Id;

			DbContext.Quizzes.Add(quiz);

			DbContext.SaveChanges();

			return new JsonResult(quiz.Adapt<QuizViewModel>(), new JsonSerializerSettings { Formatting = Formatting.Indented });


      }

      [HttpPut]
      public IActionResult Put([FromBody]QuizViewModel model)
      {

			if (model == null)
				return new StatusCodeResult(500);

			var quiz = DbContext.Quizzes.Where(q => q.Id == model.Id).FirstOrDefault();

			if (quiz == null)
			{
				return NotFound(new { Error = string.Format("nie znaleziono quizu o id: {0}", model.Id )
				});
			}

			quiz.Title = model.Title;
			quiz.Description = model.Description;
			quiz.Notes = model.Description;
			quiz.Text = model.Text;

			quiz.LastModifiedDate = DateTime.Now;

			DbContext.SaveChanges();

			return new JsonResult(quiz.Adapt<QuizViewModel>(), new JsonSerializerSettings { Formatting = Formatting.Indented });


      }

      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
			var quiz = DbContext.Quizzes.Where(q => q.Id == id).FirstOrDefault();

			if (quiz == null)
			{
				return NotFound(new { Error = string.Format("nie znaleziono quizu o id: {0}", id)
				});
			}

			DbContext.Remove(quiz);
			DbContext.SaveChanges();

			return new NoContentResult();
      }
		#endregion

		#region Metody routingu bazujące na atrybutach

		[HttpGet("Latest/{num?}")]
		public IActionResult Latest(int num = 10)
		{
			var lastest = DbContext.Quizzes.OrderByDescending(q => q.CreatedDate)
											.Take(num)
											.ToArray();
											

			return new JsonResult(lastest.Adapt<QuizViewModel[]>(), new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented
			});




		}

		[HttpGet("ByTitle/{num:int?}")]
		public IActionResult ByTitle(int num = 10)
		{
			var bytitle = DbContext.Quizzes.OrderBy(q => q.Title).Take(num).ToArray();

			return new JsonResult(bytitle.Adapt<QuizViewModel[]>(), new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented
			});
		}

		[HttpGet("Random/{num:int?}")]
		public IActionResult Random(int num = 10)
		{

			var random = DbContext.Quizzes.OrderBy(q => Guid.NewGuid()).Take(num).ToArray();
			return new JsonResult(random.Adapt<QuizViewModel[]>(), new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented
			});
		}
		#endregion
	}
}