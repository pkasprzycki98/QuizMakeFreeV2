﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizMakeFree.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QuizMakeFree.Data
{
	public static class DbSeeder
	{



	#region Metody publiczne

		public static void Seed(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			if (!dbContext.Users.Any())
			{
				CreateUsers(dbContext, roleManager, userManager).GetAwaiter().GetResult();
			}
			if (!dbContext.Quizzes.Any()) CreateQuizzes(dbContext);
		}


		#endregion

		#region Metody generujace

		private static async Task CreateUsers(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			DateTime createdTime = new DateTime(2019,10,18);
			DateTime latModifiedDate = DateTime.Now;

			string role_Admin = "Admin";
			string role_User = "User";

			if (!await roleManager.RoleExistsAsync(role_Admin))
			{
				await roleManager.CreateAsync(new IdentityRole(role_Admin));
			}
			if (!await roleManager.RoleExistsAsync(role_User))
			{
				await roleManager.CreateAsync(new IdentityRole(role_User));
			}


			var user_admin = new ApplicationUser()
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = "Admin",
				Email = "admin@quizmakefree.com",
				CreatedDate = createdTime,
				LastModifiedDate = latModifiedDate
			};

			if (await userManager.FindByNameAsync(user_admin.UserName) == null)
			{
				await userManager.CreateAsync(user_admin, "Pass4Admin");
				await userManager.AddToRoleAsync(user_admin, role_User);
				await userManager.AddToRoleAsync(user_admin, role_Admin);

				user_admin.EmailConfirmed = true;
				user_admin.LockoutEnabled = false;
			}
		

#if DEBUG
			var user_Ryan = new ApplicationUser()
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = "Ryan",
				Email = "ryan@quizmakefree.com",
				CreatedDate = createdTime,
				LastModifiedDate = latModifiedDate
			};
			

			var user_Vodan = new ApplicationUser()
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = "Vodan",
				Email = "Vodan@quizmakefree.com",
				CreatedDate = createdTime,
				LastModifiedDate = latModifiedDate
			};
		

			var user_Solice = new ApplicationUser()
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				UserName = "Solice",
				Email = "Solice@quizmakefree.com",
				CreatedDate = createdTime,
				LastModifiedDate = latModifiedDate
			};

			if (await userManager.FindByNameAsync(user_Ryan.UserName) == null)
			{
				await userManager.CreateAsync(user_Ryan, "Pass4Ryan");
				await userManager.AddToRoleAsync(user_Ryan, role_User);			

				user_admin.EmailConfirmed = true;
				user_admin.LockoutEnabled = false;
			}

			if (await userManager.FindByNameAsync(user_Vodan.UserName) == null)
			{
				await userManager.CreateAsync(user_Vodan, "Pass4Vodan");
				await userManager.AddToRoleAsync(user_Vodan, role_User);

				user_admin.EmailConfirmed = true;
				user_admin.LockoutEnabled = false;
			}

			if (await userManager.FindByNameAsync(user_Solice.UserName) == null)
			{
				await userManager.CreateAsync(user_Solice, "Pass4Solice");
				await userManager.AddToRoleAsync(user_Solice, role_User);

				user_admin.EmailConfirmed = true;
				user_admin.LockoutEnabled = false;
			}

#endif
			await dbContext.SaveChangesAsync();
		}

		private static void CreateQuizzes(ApplicationDbContext dbContext)
		{
			DateTime dateTime = new DateTime(2018, 1, 1);
			DateTime lastModifiedDate = DateTime.Now;

			var authorId = dbContext.Users
				.Where(u => u.UserName == "Admin")
				.FirstOrDefault()
				.Id;
#if DEBUG
			var num = 47;

			for (int i = 1; i <= num; i++)
			{
				CreateSampleQuiz(
					dbContext,
					i,
					authorId,
					num - 1,
					3,
					3,
					3,
					dateTime.AddDays(-num));
			}

#endif
			EntityEntry<Quiz> e1 = dbContext.Quizzes.Add(new Quiz()
			{
				UserId = authorId,
				Title = "Jesteś po Jasnej czy Ciemnej stronie Mocy?",
				Description = "Test osobowości bazujacy na Gwiezdnych wojnach",
				Text = @"Mądrze wybrać musisz, młody padawanie: " +
				"ten test sprawdzi, czy twoja wola jest na tyle silnie," +
				"aby pasować do zasad Jasnej strony Mocy, czy też" +
				"jesteś skazany na skuszenie sie na Ciemną stronę." +
				"Jeśli chcesz zostać prawdziwym JEDI, nie możesz pominąć takiej szansy!",
				ViewCount = 2343,
				CreatedDate = dateTime,
				LastModifiedDate = lastModifiedDate

			});

			EntityEntry<Quiz> e2 = dbContext.Quizzes.Add(new Quiz()
			{

				UserId = authorId,
				Title = "Pokolenie X,Y czy Z?",
				Description = "Dowiedz się, do której dekady najlepiej pasujesz",
				Text = @"Czy czujesz się dobrze w swoim pokoleniu?" +
						"W którym roku powinienneś się urodzić?" +
						"Oto kilka pytań, które pozwolą Ci tego dowiedzieć",
				ViewCount = 4180,
				CreatedDate = dateTime,
				LastModifiedDate = lastModifiedDate,


			});

			EntityEntry<Quiz> e3 = dbContext.Quizzes.Add(new Quiz()
			{
				UserId = authorId,
				Title = "Którą postacią z Shingeki No Kyojin jesteś?",
				Description = "Test osobowości bazujacy na Ataku tytanów",
				Text = @"Czy niestrudzenie szukasz zemsty jak Eren?" +
						"Czy będziesz się narażac, aby chronić swoich przyjaciół jak Mikasa?" +
						"Czy ufasz swoim umiejętnościom walki jak Levi," +
						"czy raczej wolisz polegać na strategiach i taktyca jak Arwin?" +
						"Odkryj prawdziewgo siebie dzięki temu testowi osobwości bazującemu na Ataku tytanów!",
				ViewCount = 5203,
				CreatedDate = dateTime,
				LastModifiedDate = lastModifiedDate

			});

			dbContext.SaveChanges();
		}


		#endregion

		#region metody pomocnicze

		private static void CreateSampleQuiz(ApplicationDbContext dbContext,
			int num,
			string authorId,
			int ViewCount,
			int numberOfQuestions,
			int numberOfAnswerPerQuestion,
			int numberOfResult,
			DateTime createdDate)
		{

			var quiz = new Quiz()
			{
				UserId = authorId,
				Title = string.Format("Tytuł quizy {0}", num),
				Description = string.Format("To jest przykładowy opis quizy {0}", num),
				Text = "To jest przykładowy quiz utworzony przez klase DbSeeder w celach testowych." +
						"Wszystkie pytania, odpowiedzi i wyniki również zostały wygenerowane automatycznie",
				ViewCount = ViewCount,
				CreatedDate = createdDate,
				LastModifiedDate = createdDate


			};

			dbContext.Quizzes.Add(quiz);

			for (int i=0;i< numberOfQuestions;i++)
			{
				var question = new Question()
				{
					QuizId = quiz.Id,
					Text = "To jest przykładowe pytanie utworzone przez klasę DbSeeder w celach testowych." +
							"Wszystkie odpowiedzi do pytania również są wygenerowane automatycznie.",
					CreatedDate = createdDate,
					LastModifiedDate = createdDate
				};

				dbContext.Questions.Add(question);
				dbContext.SaveChanges();

				for (int i2 = 0; i2 < numberOfAnswerPerQuestion; i2++)
				{
					var e2 = dbContext.Answers.Add(new Answer()
					{

						QuestionId = question.Id,
						Text = "To jest przykładowa odpowiedź utworzona przez klasę DbSeeder w celach testowych",
						Value = i2,
						CreatedDate = createdDate,
						LastModifiedDate = createdDate
					});
				}
				

			}

			for (int i = 0; i < numberOfResult; i++)
			{
				dbContext.Results.Add(new Result()
				{

					QuizId = quiz.Id,
					Text = "To jest przykładowy wynik utworzony przez klasę DbSeeder w celach testowych.",
					MinValue = 0,
					MaxValue = numberOfAnswerPerQuestion * 2,
					CreatedDate = createdDate,
					LastModifiedDate = createdDate,
				});
			}

			dbContext.SaveChanges();

		}
		

		
		#endregion
	}
}
