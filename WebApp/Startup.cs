using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Models;
using WebApp.Queries;
using WebApp.Types;

namespace WebApp
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
         services.AddDbContext<QLContext>(x =>
         {
            x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
         });         
         services.AddGraphQL(
            SchemaBuilder.New()
               .AddQueryType<Query>()
               .AddType<StateType>()
               .AddType<CountryType>()
               .Create()
         );
         new QueryExecutionOptions { TracingPreference = TracingPreference.Always };
         //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      }
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         //if (env.IsDevelopment())
         //{
         //   app.UseDeveloperExceptionPage();
         //}
         //app.UseMvc();
         app.UseGraphQL(new QueryMiddlewareOptions { Path = "/graphql" });
         app.UsePlayground(new PlaygroundOptions { Path = "/graphql" });
      }
   }
}
