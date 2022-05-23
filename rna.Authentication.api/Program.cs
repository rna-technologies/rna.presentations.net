using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //.UseApplicationInsights()
            .UseConfiguration(Configuration)
            .UseStartup<Startup>()
            .Build();

        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Constant.HostingFilePath, true, true)
            .Build();
    }
}
