using Api.Schemas.Misc;
using GraphQL.Types;
using System.Collections.Generic;

namespace Api.Schemas.Mutations
{
    class CompositeMutation : ObjectGraphType
    {
        public CompositeMutation(IEnumerable<IGraphMutationMarker> mutationMarkers, IApiApplication api)
        {
            Name = "ApplicationMutation";
            foreach (var marker in mutationMarkers)
            {
                marker.SetupMutations(this, api);
            }
        }
    }
}
