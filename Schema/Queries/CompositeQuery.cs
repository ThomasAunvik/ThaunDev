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
        public CompositeQuery(IEnumerable<IGraphQueryMarker> queryMarkers)
        {
            Name = "CompositeQuery";
            foreach(var marker in queryMarkers)
            {
                var q = marker as ObjectGraphType;
                foreach (var f in q.Fields)
                {
                    if (f.RequiresAuthorization())
                    {
                        Console.WriteLine(f.GetPolicies());
                    }
                    AddField(f);
                }
            }
        }
    }
}
