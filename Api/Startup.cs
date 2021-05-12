using Api.Middlewares;
using Api.Schemas;
using Api.Schemas.Misc;
using Api.Schemas.Queries;
using Application.Users.Query;
using Domain.Entities;
using Domain.GraphTypes;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Api.Extentions;
using System;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var appUrl = Configuration.GetSection("Urls").GetValue<string>("App");

            var apiConnectionStringPostfix = "ApiConnection";
            var apiConnectionString = Configuration.GetConnectionString(apiConnectionStringPostfix);

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(apiConnectionString));

            services
                .AddSingleton<IApiApplication, ApiApplication>()
                .AddSingleton<ApplicationSchema>()
                .AddGraphQL((options, provider) =>
                {
                    options.EnableMetrics = Environment.IsDevelopment();
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occured", ctx.OriginalException.Message);
                })
                .AddDefaultEndpointSelectorPolicy()
                .AddSystemTextJson()
                .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User })
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.IsDevelopment())
                .AddWebSockets()
                .AddDataLoader()
                .AddGraphTypes(typeof(GraphUserType));

            services.AddMediatR(typeof(Details).Assembly);
            services.AddAutoMapper(typeof(User).Assembly);

            services.SetupGraphQLServices();
            services.AddGraphQLAuth((settings, provider) => settings.AddPolicy("AdminPolicy", p => p.RequireClaim("role", "Admin")));

            services.AddCors((o) =>
            {
                o.AddPolicy("default", p =>
                {
                    p.WithOrigins(appUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("default");

            app.UseWebSockets();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQLWebSockets<ApplicationSchema>();
                endpoints.MapGraphQL<ApplicationSchema, GraphQLHttpMiddlewareWithLogs<ApplicationSchema>>();

                endpoints.MapGraphQLPlayground();
            });
        }
    }
}
