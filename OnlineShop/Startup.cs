using System.IO;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using OnlineShop.Extensions;
using OnlineShop.Helpers;
using OnlineShop.Middleware;
using StackExchange.Redis;

namespace OnlineShop
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocumentation();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<ShopContext>(x =>
                x.UseNpgsql(_configuration.GetConnectionString("DefaultString")));
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseNpgsql(_configuration.GetConnectionString("IdentityConnection"));
            });
            
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(_configuration.GetConnectionString("Redis"),true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig"));
            services.AddApplicationServices();
            services.AddIdentityServices(_configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opt => opt.NoReferrer());
            app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
            app.UseXfo(opt => opt.Deny());
            app.UseCsp(opt => opt
                .BlockAllMixedContent()
                .FormActions(s => s.Self().CustomSources("https://fonts.googleapis.com/"))
                .FrameAncestors(s => s.Self())
                .ImageSources(s => s.Self().CustomSources("data:"))
                
            );

            app.Use(async (context, next) => 
            {
                context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
                await next.Invoke();
            });

            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseSwaggerDocumentation();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions 
            {
                FileProvider = new PhysicalFileProvider
                (
                    Path.Combine(Directory.GetCurrentDirectory(), "Content")     
                ), RequestPath = "/content"
            });
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index","FallBack");
            });
        }
    }
}
