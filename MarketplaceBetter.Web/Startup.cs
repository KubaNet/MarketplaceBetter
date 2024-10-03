using Autofac;
using MarketplaceBetter.Infrastructure.Data;
using MarketplaceBetter.Services.Domain.Catalog.Products;
using MarketplaceBetter.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using System;
using System.Linq;
using System.Reflection;

namespace MarketplaceBetter.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor().AddHubOptions(options => { options.DisableImplicitFromServicesParameters = true; });
            services.AddMudServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<BetterDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BetterConnection")));
            services.AddScoped<AuthenticationStateProvider, BetterAuthenticationStateProvider>();
            services.AddHttpContextAccessor();
            services.AddSignalR(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(30);
                options.HandshakeTimeout = TimeSpan.FromSeconds(60);
                options.KeepAliveInterval = TimeSpan.FromMinutes(30);
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here.
            builder.RegisterType<BetterDbContext>().As<IDbContext>();
            builder.RegisterType<UnitOfWork<BetterDbContext>>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BrandService))).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Startup))).Where(t => t.Name.EndsWith("Controller")).InstancePerLifetimeScope();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
