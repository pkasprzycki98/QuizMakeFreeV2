﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMakeFree.Data.Models
{
	public class Quiz
	{
		#region Kontruktor

		public Quiz()
		{

		}

		#endregion

		#region Właściowości
		[Key]
		[Required]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		public string Text { get; set; }
		public string Notes { get; set; }
		[DefaultValue(0)]
		public int Type { get; set; }
		[DefaultValue(0)]
		public int Flags { get; set; }

		[Required]
		public string UserId { get; set; }
		[Required]
		public string ViewCount { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		[Required]
		public DateTime LastModifedDate { get; set; }
		#endregion

		#region Właściowości wczytywane lazyLoading

		[ForeignKey("UserId")]
		public virtual ApplicationUser User { get; set; }

		public virtual List<Question> Questions { get; set; }

		public virtual List<Result> Results { get; set; }

		#endregion
	}
}
