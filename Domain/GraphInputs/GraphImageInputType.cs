using Domain.GraphObjects;
using GraphQL.Types;

namespace Domain.GraphInputs
{
    public class GraphImageInputType : InputObjectGraphType<GraphImage>
    {
        public GraphImageInputType()
        {
            Field(o => o.Name);
            Field(o => o.Data);
        }
    }
}
