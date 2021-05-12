using Api.Schemas.Misc;
using Domain.GraphTypes;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas.Queries
{
    public class UserQueries : ObjectGraphType, IGraphQueryMarker
    {
        private readonly IApiApplication _api;

        public UserQueries(IApiApplication api)
        {
            _api = api;

            FieldAsync<GraphUserType>("current",
                resolve: async (context) =>
                {
                    object name;
                    context.UserContext.TryGetValue("name", out name);
                    return await _api.Users.GetCurrentUser();
                });

            this.AuthorizeWith("AdminPolicy");
            Field<ListGraphType<GraphUserType>>("users", resolve: context => api.Users.AllUsers.Take(100));

            this.AuthorizeWith("AdminPolicy");
            FieldAsync<GraphUserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: async (context) =>
                {
                    var recievedId = context.GetArgument<int>("id");
                    return await _api.Users.GetUser(recievedId);
                });
        }
    }
}
