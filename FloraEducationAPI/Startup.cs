using FloraEducationAPI.Context;
using FloraEducationAPI.Repository.Implementations;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Implementations;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FloraEducationAPI
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
            services.AddControllers();

            // Database connection

            var dbConnectionString = string.Empty;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                dbConnectionString = Configuration.GetConnectionString("FloraEducationApiDatabase");
            }
            else
            {
                // Use connection string provided at runtime by Heroku.
                var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                connectionUrl = connectionUrl.Replace("postgres://", string.Empty);

                var pgUserPass = connectionUrl.Split("@")[0];
                var pgHostPortDb = connectionUrl.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];

                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPass = pgUserPass.Split(":")[1];
                var pgHost = pgHostPort.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];

                dbConnectionString = $"Host={pgHost};Database={pgDb};Port={pgPort};Username={pgUser};Password={pgPass};SSL Mode=Require;Trust Server Certificate=true";
            }

            services.AddDbContext<FloraEducationDbContext>(options => options.UseNpgsql(dbConnectionString));

            // Repositories 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMiniQuizRepository, MiniQuizRepository>();
            services.AddScoped<IPlantRepository, PlantRepository>();


            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPlantService, PlantService>();
            services.AddTransient<IMiniQuizService, MiniQuizService>();
            services.AddTransient<IBadgeService, BadgeService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserLikedPlantsService, UserLikedPlantsService>();

            // JSON config
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // CORS
            services.AddCors();
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

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
