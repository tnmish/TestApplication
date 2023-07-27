using Microsoft.EntityFrameworkCore;
using TestApplication.Database;
using TestApplication.Services;
using TestApplication.Config;

namespace TestApplication
{
    public class Startup
    {
        public  IConfiguration AppConfig { get; }
        public Startup (IConfiguration config)
        {
            AppConfig = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // устанавливаем контекст данных
           
            string con = AppConfig.GetSection ("ConnectionString").Value??"" ;
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(con));
            services.AddScoped  (typeof(IRepository<>), typeof(Repository<>));

            services.Configure<MailConfig>(AppConfig.GetSection("Mail"));
            services.Configure<TelegramConfig>(AppConfig.GetSection("Telegram"));

            services.AddTransient<ISendMessage, SendMessageMail>();
            services.AddTransient<ISendMessage, SendMessageTelegram>();
            services.AddTransient<ISendMessage, SendMessageWebcab>();            
            //services.AddTransient<ISendMessage, SendMessageTD>();
            services.AddControllers(); // используем контроллеры без представлений
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();           
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });
        }
    }
}
