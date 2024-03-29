﻿using Api.Schemas.Mutations;
using Api.Schemas.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Schemas.Misc
{
    public static class InitializeQueryServices
    {
        public static IServiceCollection SetupGraphQLServices(this IServiceCollection services)
        {
            services.AddSingleton<IGraphQueryMarker, UserQueries>();
            services.AddSingleton<IGraphMutationMarker, UserMutations>();
            return services;
        }
    }
}
