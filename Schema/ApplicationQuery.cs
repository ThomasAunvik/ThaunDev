using Api.Schemas.Misc;
using Api.Schemas.Queries;
using Application.Users.Query;
using Domain.GraphObjects;
using Domain.GraphTypes;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Schemas
{
    public class ApplicationQuery : ObjectGraphType, IGraphQueryMarker
    {
        private readonly IApiApplication _api;

        public ApplicationQuery(IApiApplication api)
        {
            _api = api;
        }
    }
}
