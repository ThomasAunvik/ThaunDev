using Api.Schemas.Misc;
using Api.Schemas.Queries;
using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.Schemas
{
    public class ApplicationSchema : Schema
    {
        public ApplicationSchema(IApiApplication api, IServiceProvider provider) : base(provider)
        {
            Query = new CompositeQuery(provider.GetServices<IGraphQueryMarker>());
            Mutation = new ApplicationMutation(api);
        }
    }
}
