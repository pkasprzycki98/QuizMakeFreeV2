using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizMakeFreeWebApp.ViewModels;
using System.Collections.Generic;

namespace QuizMakeFreeWebApp.Controllers
{
   [Route("api/[controller]")]
   public class ResultController : Controller
   {
      #region Metody dostosowujące do konwencji REST
      /// <summary>
      /// Pobiera wynik o podanym {id}
      /// </summary>
      /// <param name="id">identyfikator istniejącego wyniku</param>
      /// <returns>wynik o podanym {id}</returns>
      [HttpGet("{id}")]
      public IActionResult Get(int id)
      {
         return Content("(Jeszcze) niezaimplementowane!");
      }

      /// <summary>
      /// Dodaje wynik do bazy danych
      /// </summary>
      /// <param name="model">obiekt ResultViewModel z danymi do wstawienia</param>
      [HttpPut]
      public IActionResult Put(ResultViewModel model)
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Modyfikuje wynik o podanym {id}
      /// </summary>
      /// <param name="model">obiekt ResultViewModel z danymi do uaktualnienia</param>
      [HttpPost]
      public IActionResult Post(ResultViewModel model)
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Usuwa z bazy danych wynik o podanym {id}
      /// </summary>
      /// <param name="id">identyfikator istniejącego wyniku</param>
      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
         throw new NotImplementedException();
      }
      #endregion

      
      [HttpGet("All/{quizId}")]
      public IActionResult All(int quizId)
      {
         var sampleResults = new List<ResultViewModel>();

        
         sampleResults.Add(new ResultViewModel()
         {
            Id = 1,
            QuizId = quizId,
            Text = "Co cenisz w swoim życiu najbardziej?",
            CreatedDate = DateTime.Now,
            LastModifiedDate = DateTime.Now
         });

         
         for (int i = 2; i <= 5; i++)
         {
            sampleResults.Add(new ResultViewModel()
            {
               Id = i,
               QuizId = quizId,
               Text = String.Format("Przykładowe pytanie {0}", i),
               CreatedDate = DateTime.Now,
               LastModifiedDate = DateTime.Now
            });
         }

        
         return new JsonResult(
             sampleResults,
             new JsonSerializerSettings()
             {
                Formatting = Formatting.Indented
             });
      }
   }
}

