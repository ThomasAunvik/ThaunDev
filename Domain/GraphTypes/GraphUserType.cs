using Domain.GraphObjects;
using GraphQL.Types;

namespace Domain.GraphTypes
{
    public class GraphUserType : ObjectGraphType<GraphUser>
    {
        public GraphUserType()
        {
            Field(o => o.Id);
            Field(o => o.Username);
            Field(o => o.FirstName);
            Field(o => o.LastName);
        }
    }
}
