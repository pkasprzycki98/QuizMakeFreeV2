using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMakeFree.Data.Models
{
	public class ApplicationUser
	{
		#region Konstruktor
		public ApplicationUser()
		{
			
		}
		#endregion

		#region Właściwości

		[Key]
		[Required]
		public string Id { get; set; }
		[Required]
		[MaxLength]
		public string UserName { get; set; }
		[Required]
		public string Email { get; set; }
		public string DisplayName { get; set; }
		public string Notes { get; set; }
		[Required]
		public int Type { get; set; }
		[Required]
		public int Flags { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
		#endregion

		#region Właściwości wczytwane lazyLoading
		[ForeignKey("QuizId")]
		public virtual List<Quiz> Quizzes { get; set; }
		#endregion

	}
}
