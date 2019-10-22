﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMakeFree.Data.Models
{
	public class Answer
	{
		#region Kontruktor

		public Answer()
		{

		}

		#endregion

		#region Właściowości
		[Key]
		[Required]
		public int Id { get; set; }
		[Required]
		public int QuestionId { get; set; }
		public string Text { get; set; }
		[Required]
		public int Value { get; set; }
		public string Notes { get; set; }
		[DefaultValue(0)]
		public int Type { get; set; }
		[DefaultValue(0)]
		public int Flags { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }
		[Required]
		public DateTime LastModifiedDate { get; set; }
		#endregion

		#region Właściwości wczytwane lazyLoading
		[ForeignKey("QuestionId")]
		public virtual Question Question { get; set; }
		#endregion
	}
}
