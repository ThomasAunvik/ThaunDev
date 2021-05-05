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

namespace Api.Schemas
{
    class ApplicationMutation : ObjectGraphType<object>
    {
        public ApplicationMutation(IApiApplication api)
        {
            Field<GraphUserType>("adduser",
                arguments: new QueryArguments(
                    new QueryArgument<GraphUserInputType> { Name = "user" }
                ),
                resolve: context => {
                    var recievedUser = context.GetArgument<GraphUser>("user");
                    var user = api.Users.AddUser(recievedUser);
                    return user;
                });
        }
    }
}
