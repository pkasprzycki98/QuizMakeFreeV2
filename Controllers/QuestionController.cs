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
      /// <summary>
      /// Pobiera pytanie o podanym {id}
      /// </summary>
      /// <param name="id">identyfikator istniejącego pytania</param>
      /// <returns>pytanie o podanym {id}</returns>
      [HttpGet("{id}")]
      public IActionResult Get(int id)
      {
         return Content("(Jeszcze) niezaimplementowane!");
      }

      /// <summary>
      /// Dodaje nowe pytanie do bazy danych
      /// </summary>
      /// <param name="model">obiekt QuestionViewModel z danymi do wstawienia</param>
      [HttpPut]
      public IActionResult Put(QuestionViewModel model)
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Modyfikuje pytanie o podanym {id}
      /// </summary>
      /// <param name="model">obiekt QuestionViewModel z danymi do uaktualnienia</param>
      [HttpPost]
      public IActionResult Post(QuestionViewModel model)
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Usuwa z bazy danych pytanie o wskazanym identyfikatorze
      /// </summary>
      /// <param name="id">identyfikator istniejącego pytania</param>
      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
         throw new NotImplementedException();
      }
      #endregion

      // GET api/question/all
      [HttpGet("All/{quizId}")]
      public IActionResult All(int quizId)
      {
         var sampleQuestions = new List<QuestionViewModel>();

         // Dodaj pierwsze przykładowe pytanie
         sampleQuestions.Add(new QuestionViewModel()
         {
            Id = 1,
            QuizId = quizId,
            Text = "Co cenisz w swoim życiu najbardziej?",
            CreatedDate = DateTime.Now,
            LastModifiedDate = DateTime.Now
         });

         // Dodaj kilka innych przykładowych pytań
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

         // Przekaż wyniki w formacie JSON
         return new JsonResult(
             sampleQuestions,
             new JsonSerializerSettings()
             {
                Formatting = Formatting.Indented
             });
      }
   }
}

