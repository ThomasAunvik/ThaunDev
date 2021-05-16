using Domain.GraphObjects;
using GraphQL.Types;

namespace Domain.GraphInputs
{
    public class GraphUserInputType : InputObjectGraphType<GraphUser>
    {
        public GraphUserInputType()
        {
            Field(o => o.Id);
            Field(o => o.Username);
            Field(o => o.FirstName);
            Field(o => o.LastName);
            Field(o => o.Email);

            Field(o => o.ProfilePicture, type: typeof(GraphImageInputType), nullable: true);
        }
    }
}
