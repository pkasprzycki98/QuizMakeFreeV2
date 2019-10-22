using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFreeWebApp.ViewModels;
using System.Collections.Generic;

namespace QuizMakeFreeWebApp.Controllers
{
   [Route("api/[controller]")]
   public class QuestionController : Controller
   {
      #region Metody dostosowujące do konwencji REST
      
      [HttpGet("{id}")]
      public IActionResult Get(int id)
      {
         return Content("(Jeszcze) niezaimplementowane!");
      }

      
      [HttpPut]
      public IActionResult Put(QuestionViewModel model)
      {
         throw new NotImplementedException();
      }

     
      [HttpPost]
      public IActionResult Post(QuestionViewModel model)
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

