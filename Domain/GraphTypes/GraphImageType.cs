using Domain.GraphObjects;
using GraphQL.Types;

namespace Domain.GraphTypes
{
    public class GraphImageType : ObjectGraphType<GraphImage>
    {
        public GraphImageType()
        {
            Field(o => o.Name);
            Field(o => o.Data);
        }
    }
}
