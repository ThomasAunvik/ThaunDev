using Api.Schemas.Misc;
using Domain.GraphInputs;
using Domain.GraphObjects;
using Domain.GraphTypes;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas.Mutations
{
    public class UserMutations : IGraphMutationMarker
    {
        public void SetupMutations(ObjectGraphType graph, IApiApplication api)
        {
            graph.Field<GraphUserType>("adduser",
                arguments: new QueryArguments(
                    new QueryArgument<GraphUserInputType> { Name = "user" }
                ),
                resolve: context =>
                {
                    var recievedUser = context.GetArgument<GraphUser>("user");
                    var user = api.Users.AddUser(recievedUser);
                    return user;
                })
                .AuthorizeWith("Admin");

            graph.FieldAsync<GraphUserType>("edituser",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" },
                    new QueryArgument<GraphEditUserInputType> { Name = "user" }
                ),
                resolve: async (context) =>
                {
                    var recievedId = context.GetArgument<int>("id");
                    var recievedUser = context.GetArgument<GraphUser>("user");

                    return await api.Users.EditUser(recievedId, recievedUser);
                })
                .AuthorizeWith("Authorized");
        }
    }
}
