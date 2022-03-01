using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rna.Core.Identity.Infrastructure.Models;
//using Resource.Application;
using rna.Core.Infrastructure.Logics.Users.Verifications.ContactSignInVerification;
using rna.Core.Infrastructure.Services.MiddleWare;
using System.Reflection;

namespace rna.Authentication.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var modelBuilder = new ModelBuilder();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
            modelBuilder.Entity<GroupRelationModel>().ToTable(g => g.ExcludeFromMigrations());
            //Configuration, Env,
            services.AddRnaServices(new()
            {
                Configuration = Configuration,
                Environment = Env,
                //ModelBuilder = modelBuilder
            });

            services.AddMediatR(typeof(VerifyUserEmailHandler).GetTypeInfo().Assembly);

            services.AddAuthorization();

            services.AddApplicationInsightsTelemetry();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //if (Env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}


            var origins = Configuration.GetSection("ClientBaseUrls").Get<string[]>();

            //app.UseCors(x => x.WithOrigins(origins).AllowAnyMethod().AllowCredentials().AllowAnyHeader());

            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowCredentials().AllowAnyHeader());

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyOrigin().WithOrigins(origins));


            //app.UseAuthentication();
            //app.UseMvc();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
