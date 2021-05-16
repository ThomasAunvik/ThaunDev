using Domain.GraphObjects;
using GraphQL.Types;

namespace Domain.GraphInputs
{
    public class GraphEditUserInputType : InputObjectGraphType<GraphUser>
    {
        public GraphEditUserInputType()
        {
            Field(o => o.Id, nullable: true);
            Field(o => o.Username, nullable: true);
            Field(o => o.FirstName, nullable: true);
            Field(o => o.LastName, nullable: true);
            Field(o => o.Email, nullable: true);

            Field(o => o.ProfilePicture, type: typeof(GraphImageInputType), nullable: true);
        }
    }
}
