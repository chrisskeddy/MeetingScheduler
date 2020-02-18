using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using MeetingScheduler.Models;
//Following tutorial:
// https://damienbod.com/2016/01/11/asp-net-5-with-postgresql-and-entity-framework-7/
// https://medium.com/faun/asp-net-core-entity-framework-core-with-postgresql-code-first-d99b909796d7
namespace MeetingScheduler
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
      //services.AddControllers().AddNewtonsoftJson();
      // Use a PostgreSQL database
      services.AddDbContext<MeetingSchedulerContext>(opt =>
          opt.UseNpgsql(Configuration.GetConnectionString("PostgreSQLConection")));
      //services.AddControllers().AddNewtonsoftJson();
      /*
      services.AddMvc().AddJsonOptions(o =>
               {
                 o.JsonSerializerOptions.PropertyNamingPolicy = null;
                 o.JsonSerializerOptions.DictionaryKeyPolicy = null;
               });
               */
      services.AddControllersWithViews();
      services.AddDistributedMemoryCache();


      services.AddMvc();

      services.ConfigureApplicationCookie(options =>
      {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = ".AspNetCore.Session";
        options.Cookie.Expiration = TimeSpan.FromMinutes(30);
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
      });
      services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.IsEssential = true;
        options.IOTimeout = TimeSpan.FromMinutes(30);
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseSession();
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();

      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
