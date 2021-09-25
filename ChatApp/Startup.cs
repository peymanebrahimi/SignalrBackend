using ChatApp.Data;
using ChatApp.Data.Expense;
using ChatApp.Hubs;
using ChatApp.Models;
using ChatApp.Models.Expense;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Linq;

namespace ChatApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatApp", Version = "v1" });
            });
            services.AddSignalR();


            #region Mongodb

            ConventionRegistry.Register("Camel Case",
                new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);

            var mongodbCn = Configuration["ConnectionStrings:Mongodb"];

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(mongodbCn));

            services.AddTransient<IRepository<Received>, ExpenseRepository>();
            #endregion

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // IdentityServer with an additional AddApiAuthorization helper method that
            // sets up some default ASP.NET Core conventions on top of IdentityServer
            services.AddIdentityServer(o =>
                {

                })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(o =>
                {

                });

            // Authentication with an additional AddIdentityServerJwt helper method that
            // configures the app to validate JWT tokens produced by IdentityServer
            services.AddAuthentication(o =>
                {

                })
                .AddIdentityServerJwt();

            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>,
                    ConfigureJwtBearerOptions>());

            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatApp v1"));
            }
            app.UseCors(builder =>
            {
                var clients = Configuration.GetSection("SignalrOrigins")
                    .GetChildren()
                    .Select(x => x.Value)
                    .ToArray();

                builder.WithOrigins(clients)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.ConfigureExceptionHandler();

            // to chrome and adge function properly on login redirect.
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/ChatHub");
            });
        }
    }
}
