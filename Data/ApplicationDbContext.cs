using Microsoft.EntityFrameworkCore;
using QuizMakeFree.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMakeFree.Data
{
	public class ApplicationDbContext : DbContext
	{
		#region Kontruktor

		public ApplicationDbContext(DbContextOptions options): base(options)
		{

		}
		#endregion

		#region Metody

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//ApplicationUser
			modelBuilder.Entity<ApplicationUser>().ToTable("Users");
			modelBuilder.Entity<ApplicationUser>().HasMany(k => k.Quizzes).WithOne(i => i.User);

			//Quiz
			modelBuilder.Entity<Quiz>().ToTable("Quizzes");
			modelBuilder.Entity<Quiz>().Property(i => i.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<Quiz>().HasOne(i => i.User).WithMany(u => u.Quizzes);
			modelBuilder.Entity<Quiz>().HasMany(i => i.Questions).WithOne(u => u.Quiz);

			//Question
			modelBuilder.Entity<Question>().ToTable("Questions");
			modelBuilder.Entity<Question>().Property(i => i.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<Question>().HasOne(i => i.Quiz).WithMany(u => u.Questions);
			modelBuilder.Entity<Question>().HasMany(i => i.Answers).WithOne(u => u.Question);

			//Answer
			modelBuilder.Entity<Answer>().ToTable("Answers");
			modelBuilder.Entity<Answer>().Property(i => i.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<Answer>().HasOne(i => i.Question).WithMany(i => i.Answers);

			//Result
			modelBuilder.Entity<Result>().ToTable("Results");
			modelBuilder.Entity<Result>().Property(i => i.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<Result>().HasOne(i => i.Quiz).WithMany(i => i.Results);

		}
		#endregion

		#region Włościwości

		public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<Quiz> Quizzes { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<Result> Results { get; set; }
		#endregion


	}
}
