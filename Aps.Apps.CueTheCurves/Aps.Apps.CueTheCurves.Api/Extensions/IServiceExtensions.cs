using Aps.Apps.CueTheCurves.Api.Data;
using Aps.Apps.CueTheCurves.Api.GraphQL.Mutations;
using Aps.Apps.CueTheCurves.Api.GraphQL.ObjectTypes;
using Aps.Apps.CueTheCurves.Api.GraphQL.Queries;
using Aps.Apps.CueTheCurves.Api.Repositories;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using Apsy.Common.Api.Auth;
using Apsy.Common.Api.Core.ApiDoc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aps.Apps.CueTheCurves.Api.Extensions
{
    public static class IServiceExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder =>
                builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()));


            services.AddSingleton<IAuthService, FirebaseAuthService>();
            var projectName = configuration["ProjectName"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://securetoken.google.com/{projectName}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://securetoken.google.com/{projectName}",
                        ValidateAudience = true,
                        ValidAudience = projectName,
                        ValidateLifetime = true
                    };
                });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddHttpContextAccessor();
            services.AddCors();

            services.AddControllersWithViews();

            services.AddSwaggerGen(s => s.SchemaFilter<SwaggerIgnoreFilter>());

            services.AddPooledDbContextFactory<DataContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DbConnection"))
                .UseLoggerFactory(new LoggerFactory(new[] { new DebugLoggerProvider() }))
                    .EnableSensitiveDataLogging());

            services.AddGraphQLServer()
                .AddQueryType<QueryType>()
                .AddTypeExtension<UserQueries>()
                .AddTypeExtension<StyleQueries>()
                .AddTypeExtension<BrandQueries>()
                .AddTypeExtension<PostQueries>()
                .AddMutationType<MutationType>()
                .AddTypeExtension<UserMutations>()
                .AddTypeExtension<PostMutations>()
                .AddTypeExtension<StyleMutations>()
                .AddTypeExtension<BrandMutations>()
                .AddTypeExtension<ClosetMutations>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .AddAuthorization()
                .AddTypes(GetEntitiesTypes().ToArray());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStyleRepository, StyleRepository>();
            services.AddScoped<IStyleService, StyleService>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IClosetRepository, ClosetRepository>();
            services.AddScoped<IClosetService, ClosetService>();
        }

        private static List<Type> GetEntitiesTypes()
        {
            var ListResponseBaseTypes = new List<Type>();
            string appNs = Assembly.GetExecutingAssembly().GetName().Name;
            string entitiesNs = $"{appNs}.Models";
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(a => a.IsClass && (a.Namespace == $"{entitiesNs}.Entities" || a.Namespace == $"{entitiesNs}.Dtos"))
                .ToList();
            types.ForEach(a => ListResponseBaseTypes.Add(typeof(CoreObjectType<>).MakeGenericType(a)));
            return ListResponseBaseTypes;
        }
    }
}
