using Api.Schemas.Misc;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas.Queries
{
    public class CompositeQuery : ObjectGraphType
    {
        public CompositeQuery(IEnumerable<IGraphQueryMarker> queryMarkers, IApiApplication api)
        {
            Name = "ApplicationQuery";
            foreach(var marker in queryMarkers)
            {
                marker.SetupQueries(this, api);
            }
        }
    }
}
