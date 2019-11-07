using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizMakeFree.Data;
using QuizMakeFree.Data.Models;
using QuizMakeFree.ViewModels;

namespace QuizMakeFree.Controllers
{
    public class TokenController : BaseApiController
    {
		#region Konstruktor
		public TokenController(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(dbContext, roleManager, userManager, configuration)
		{

		}
		#endregion
		[HttpPost("Auth")]
		public async Task<IActionResult> Auth([FromBody] TokenRequestViewModel model)
		{
			if (model == null)
			{
				return new StatusCodeResult(500);
			}

			switch (model.Grant_type)
			{
				case "password":
					return await GetToken(model);
				default:
					return new UnauthorizedResult();
			}
		}

		private async Task<IActionResult> GetToken(TokenRequestViewModel model)
		{
			try
			{
				// Sprawdź, czy istnieje użytkownik o podanej nazwie
				var user = await UserManager.FindByNameAsync(model.Username);
				// Dopóść użycie adresu e-mail w zastępstwie nazwy użytkownika
				if (user == null && model.Username.Contains("@"))
					user = await UserManager.FindByEmailAsync(model.Username);

				if (user == null
					|| !await UserManager.CheckPasswordAsync(user, model.Password))
				{
					// Użytkownik nie istnieje lub hasła nie pasują do siebie
					return new UnauthorizedResult();
				}

				// Nazwa użytkownika i hasło jest prawidłowe - utwórz token JWT

				DateTime now = DateTime.UtcNow;

				// Dodaj odpowiednie roszczenia do JWT (RFC7519).
				var claims = new[] {
					new Claim(JwtRegisteredClaimNames.Sub, user.Id),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Iat,
						new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
                  
                };

				var tokenExpirationMins =
					Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
				var issuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

				var token = new JwtSecurityToken(
					issuer: Configuration["Auth:Jwt:Issuer"],
					audience: Configuration["Auth:Jwt:Audience"],
					claims: claims,
					notBefore: now,
					expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
					signingCredentials: new SigningCredentials(
						issuerSigningKey, SecurityAlgorithms.HmacSha256)
				);
				var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

				// Zbuduj i zwróć odpowiedź
				var response = new TokenResponseViewModel()
				{
					Token = encodedToken,
					Expiration = tokenExpirationMins
				};
				return Json(response);
			}
			catch
			{
				return new UnauthorizedResult();
			}
		}

	}
}