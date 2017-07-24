using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AgeRangerWebAPI.Entities;
using AgeRangerWebAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AgeRangerWebAPI
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
         services.AddCors(o => o.AddPolicy("MyPolicy1", builder =>
         {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
         }));
         services.AddMvc();
            
         //services.AddEntityFrameworkSqlite().AddDbContext<PersonInfoContext>();

         var connectionString = Startup.Configuration["connectionStrings:ageRangerInfoDBConnectionString"];
            services.AddDbContext<PersonInfoContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IPersonInfoRepository, PersonInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, PersonInfoContext personInfoContext)
        {
            loggerFactory.AddConsole();

            personInfoContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Person, Models.PersonDto>();
            });

         // Shows UseCors with CorsPolicyBuilder.
         app.UseCors(builder => {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
         });

         app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
