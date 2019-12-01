using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging.File;
namespace Web
{
   public class Program
   {
      public static void Main(string[] args)
      {


         CreateHostBuilder(args).Build().Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
         .ConfigureLogging(configureLogging =>
         {
            configureLogging.ClearProviders();
            configureLogging.AddConsole();
         })
         .ConfigureWebHostDefaults(webBuilder =>
         {
            webBuilder.UseStartup<Startup>();
         });
   }
}
