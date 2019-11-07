using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMakeFree.ViewModels
{
	[JsonObject(MemberSerialization.OptOut)]
	public class TokenResponseViewModel
	{
		#region Kontruktor

		public TokenResponseViewModel()
		{

		}
		#endregion

		#region Włciwości

		public string Token { get; set; }
		public int Expiration { get; set; }

		#endregion
	}
}
