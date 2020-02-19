using DonutsApi.Infrastructure;
using DonutsApi.Infrastructure.ContextExtensions;
using DonutsApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DonutsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBeforeSaveChangesHandler, AuditInfoBeforeSaveChangesHandler>();
            services.AddTransient<ISaveChangesProcessor, SaveChangesProcessor>();
            services.AddScoped<IUnitOfWork, DonutContext>();
            services.AddSwaggerDocument();
            services.AddControllers();
            services.AddDbContext<DonutContext>(options => options.UseSqlServer(Configuration.GetConnectionString(nameof(DonutContext))));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
