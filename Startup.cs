using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizMakeFree.Data;
using QuizMakeFree.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace QuizMakeFree
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			// Dodanie obs³ugi EntityFramework zwi¹zanej z SqlServer
			services.AddEntityFrameworkSqlServer();

			// Dodanie ApplicationDbContext
			services.AddDbContext<ApplicationDbContext>(options =>
			   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);

			// Dodanie obs³ugi ASP.NET Identity
			services.AddIdentity<ApplicationUser, IdentityRole>(
			opts =>
			{
				opts.Password.RequireDigit = true;
				opts.Password.RequireLowercase = true;
				opts.Password.RequireUppercase = true;
				opts.Password.RequireNonAlphanumeric = false;
				opts.Password.RequiredLength = 7;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>();

			// Dodaj uwierzytelnianie za pomoc¹ tokenów JWT
			services.AddAuthentication(opts =>
			{
				opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(cfg =>
			{
				cfg.RequireHttpsMetadata = false;
				cfg.SaveToken = true;
				cfg.TokenValidationParameters = new TokenValidationParameters()
				{
				 // Konfiguracja standardowa
				 ValidIssuer = Configuration["Auth:Jwt:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"])),
					ValidAudience = Configuration["Auth:Jwt:Audience"],
					ClockSkew = TimeSpan.Zero,

				 // Prze³¹czniki zwi¹zane z bezpieczeñstwem
				 RequireExpirationTime = true,
					ValidateIssuer = true,
					ValidateIssuerSigningKey = true,
					ValidateAudience = true
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
            {
               HotModuleReplacement = true
            });
         }
         else
         {
            app.UseExceptionHandler("/Home/Error");
         }

         app.UseStaticFiles(new StaticFileOptions()
         {
            OnPrepareResponse = (context) =>
            {
                   // Wy³¹czenie stosowania pamiêci podrêcznej dla wszystkich plików statycznych. 
                   context.Context.Response.Headers["Cache-Control"] =
                    Configuration["StaticFiles:Headers:Cache-Control"];
               context.Context.Response.Headers["Pragma"] =
                       Configuration["StaticFiles:Headers:Pragma"];
               context.Context.Response.Headers["Expires"] =
                       Configuration["StaticFiles:Headers:Expires"];
            }
         });

			app.UseAuthentication();

			app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");

            routes.MapSpaFallbackRoute(
                   name: "spa-fallback",
                   defaults: new { controller = "Home", action = "Index" });
         });

		
		}
   }
}
