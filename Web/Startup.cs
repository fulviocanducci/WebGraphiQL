using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphiQl;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddEntityFrameworkSqlServer();
      services.AddDbContextPool<DatabaseAccess>(options =>
      {        
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), config =>
        {
          config.MigrationsAssembly("DataAccess");          
        });
      });
      services.AddControllers();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseCors(options => {
        options.AllowAnyOrigin();
        options.AllowAnyMethod();
        options.AllowAnyHeader();
      });
      
      app.UseGraphiQl("/graphql");

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
