using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFreeWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using QuizMakeFree.Data;
using Mapster;
using QuizMakeFree.Data.Models;

namespace QuizMakeFreeWebApp.Controllers
{
   [Route("api/[controller]")]
   public class QuestionController : Controller
   {
		#region pola prywatne


		private ApplicationDbContext DbContext;

		#endregion

		#region kontruktor

		public QuestionController(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;
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

			return new JsonResult(question.Adapt<QuestionViewModel>(), new JsonSerializerSettings { Formatting = Formatting.Indented });
      }

      
      [HttpPost]
      public IActionResult Post([FromBody]QuestionViewModel model)
      {
			if (model == null) return new StatusCodeResult(500);

			var question = model.Adapt<Question>();


			question.CreatedDate = DateTime.Now;
			question.LastModifiedDate = question.CreatedDate;

			DbContext.Questions.Add(question);

			return new JsonResult(question.Adapt<QuestionViewModel>(), new JsonSerializerSettings { Formatting = Formatting.Indented });

         throw new NotImplementedException();
      }

     
      [HttpPut]
      public IActionResult Put(QuestionViewModel model)
      {
         throw new NotImplementedException();
      }

      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
         throw new NotImplementedException();
      }
      #endregion

      [HttpGet("All/{quizId}")]
      public IActionResult All(int quizId)
      {
         var sampleQuestions = new List<QuestionViewModel>();

        
         sampleQuestions.Add(new QuestionViewModel()
         {
            Id = 1,
            QuizId = quizId,
            Text = "Co cenisz w swoim życiu najbardziej?",
            CreatedDate = DateTime.Now,
            LastModifiedDate = DateTime.Now
         });

        
         for (int i = 2; i <= 5; i++)
         {
            sampleQuestions.Add(new QuestionViewModel()
            {
               Id = i,
               QuizId = quizId,
               Text = String.Format("Przykładowe pytanie {0}", i),
               CreatedDate = DateTime.Now,
               LastModifiedDate = DateTime.Now
            });
         }

        
         return new JsonResult(
             sampleQuestions,
             new JsonSerializerSettings()
             {
                Formatting = Formatting.Indented
             });
      }
   }
}

