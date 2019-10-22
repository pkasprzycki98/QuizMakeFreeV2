using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMakeFree.Data.Models
{
	public class Result
	{
		#region Kontruktor

		public Result()
		{

		}

		#endregion

		#region Właściowości
		[Key]
		[Required]
		public int Id { get; set; }
		[Required]
		public int QuizId { get; set; }
		public string Text { get; set; }
		[Required]
		public int? MinValue { get; set; }
		public int? MaxValue { get; set; }
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
		[ForeignKey("QuizId")]
		public virtual Quiz Quiz { get; set; }
		#endregion
	}
}
