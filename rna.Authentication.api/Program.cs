using System.IO;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using rna.Core.Base.Infrastructure.Model.Constants;

namespace rna.Authentication.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Console.Title = Assembly.GetCallingAssembly().GetName().Name ?? "App";
#endif
            Host.CreateDefaultBuilder(args)
               .UseServiceProviderFactory(new AutofacServiceProviderFactory())
               .ConfigureWebHostDefaults(webBuilder => BuildWebHost(webBuilder))
               .Build()
               .Run();
        }


        public static IWebHostBuilder BuildWebHost(IWebHostBuilder builder) =>
          builder
          .UseConfiguration(Configuration)
          .UseStartup<Startup>();

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //    //.UseApplicationInsights()
        //    .UseConfiguration(Configuration)
        //    .UseStartup<Startup>()
        //    .Build();

        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Constant.HostingFilePath, true, true)
            .Build();
    }
}
