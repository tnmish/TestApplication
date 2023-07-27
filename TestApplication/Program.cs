using NLog;
using NLog.Web;

namespace TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger(); ;
            try
            {
                logger.Debug("Application started");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception during execution");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //Host.CreateDefaultBuilder(args)
            //    .ConfigureWebHostDefaults(webBuilder =>
            //    {
            //        webBuilder.UseStartup<Startup>();
            //    });
            Host.CreateDefaultBuilder(args)            
            .ConfigureAppConfiguration(webBuilder =>
            {
                webBuilder.AddJsonFile("config.json");

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging (logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .UseNLog();



    }
}

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

//var app = builder.Build();

//app.MapControllers();

//app.Run();