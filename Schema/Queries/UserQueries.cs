using Api.Schemas.Misc;
using Domain.GraphTypes;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas.Queries
{
    public class UserQueries : IGraphQueryMarker
    {
        public void SetupQueries(ObjectGraphType graph, IApiApplication api)
        {
            graph.FieldAsync<GraphUserType>("current",
                resolve: async (context) =>
                {
                    object name;
                    context.UserContext.TryGetValue(ClaimTypes.NameIdentifier, out name);

                    return await api.Users.GetCurrentUser();
                })
                .AuthorizeWith("Authorized");

            graph.Field<ListGraphType<GraphUserType>>("users", resolve: context => api.Users.AllUsers.Take(100))
                 .AuthorizeWith("Admin");

            graph.FieldAsync<GraphUserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: async (context) =>
                {
                    var recievedId = context.GetArgument<int>("id");
                    return await api.Users.GetUser(recievedId);
                })
                .AuthorizeWith("Admin");
        }
    }
}
