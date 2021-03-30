using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ProyectoSalud.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseStartup<Startup>()
                    // .UseKestrel((context, serverOptions) =>
                    // {
                    //     serverOptions.Configure(context.Configuration.GetSection("Kestrel"))
                    //         .Endpoint("HTTPS", listenOptions =>
                    //         {
                    //             listenOptions.HttpsOptions.SslProtocols = SslProtocols.Tls12;
                    //         });
                    // });
                });
    }
}
