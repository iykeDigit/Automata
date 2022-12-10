using ExpenseWebApp.API.ExtensionMethods;
using ExpenseWebApp.API.Extensions;
using ExpenseWebApp.Data.Seeder;
using ExpenseWebApp.Utilities.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExpenseWebApp.API
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
            services.InjectDependencies();

            services.AddAutoMapper(typeof(Mappings));

           services.AddCorsConfiguration();

            services.InjectFluentValidations();
            
            services.AddControllers();

            services.AddMVCConfiguration();

            services.AddSwaggerConfiguration();

            //mail configuration
            services.MailService(Configuration);

            services.AddDbContextAndConfigurations(Configuration);
           
            // Configure AutoMapper
            services.AddAutoMapper(typeof(Mappings));

            //Logging 
            services.AddLoggingConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {         
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "ExpenseWebApp.API v1");
                c.RoutePrefix = string.Empty;
            });
            
            ExpenseAppSeeder.PrepopulateDb(app).GetAwaiter().GetResult();

            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}