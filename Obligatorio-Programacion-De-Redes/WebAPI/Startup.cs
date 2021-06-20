using AdministrativeServer;
using BusinessLogic.Managers;
using DataAccess;
using GrpcServicesInterfaces;
using LogsServer;
using LogsServerInterface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace WebAPI
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
            services.AddControllers().AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }
            );
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebAPI", Version = "v1"}); });
            services.AddScoped<IPostServiceGrpc, PostServiceGrpc>();
            services.AddScoped<IThemeServiceGrpc, ThemeServiceGrpc>();
            services.AddScoped<IThemeToPostServiceGrpc, ThemeToPostServiceGrpc>();
            services.AddScoped<ILogService, LogServiceGrpc>();
            services.AddScoped<ManagerLogRepository, DataBaseLogRepository>();
            services.AddScoped<ManagerPostRepository, DataBasePostRepository>();
            services.AddScoped<ManagerThemeRepository, DataBaseThemeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}