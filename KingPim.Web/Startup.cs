using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KingPim.DataAccess;
using KingPim.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KingPim.Infrastructure;
using KingPim.Web.Helpers;

namespace KingPim.Web
{
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration conf)
        {
            _configuration = conf;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var conn = _configuration.GetConnectionString("KingPim");

            services.AddMvc();

            services.AddMvc().AddXmlSerializerFormatters();

            services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(conn, b => b.MigrationsAssembly("KingPim.Web")));

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>().
                AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            });

            services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<ProductHelper>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<CustomAppSettings>(_configuration.GetSection("CustomAppSettings"));

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext ctx, IIdentitySeeder identitySeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}"
                );
            });

            var runIdentitySeed = Task.Run(async () => await identitySeeder.CreateRolesAndAdminUserIfEmpty()).Result;

            Seed.FillIfEmpty(ctx);
        }
    }
}