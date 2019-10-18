using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizMakeFree.Data;

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

         // Dodanie obs³ugi EntityFramework zwi¹zanej z SqlServer.
         services.AddEntityFrameworkSqlServer();
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		// Dodanie ApplicationDbContext.
		services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
         );
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

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");

            routes.MapSpaFallbackRoute(
                   name: "spa-fallback",
                   defaults: new { controller = "Home", action = "Index" });
         });

			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
				dbContext.Database.Migrate();
				DbSeeder.Seed(dbContext);
			}
      }
   }
}
