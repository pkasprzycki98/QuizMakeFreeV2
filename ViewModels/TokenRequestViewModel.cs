using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMakeFree.ViewModels
{
	[JsonObject(MemberSerialization.OptOut)]
	public class TokenRequestViewModel
	{
		#region Konstruktor
		public TokenRequestViewModel()
		{

		}
		#endregion

		#region Właściowści
		public string Grant_type { get; set; }
		public string Client_id { get; set; }
		public string Client_secret { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Refresh_token { get; set; }


		#endregion
	}
}
