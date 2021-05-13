﻿using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas.Misc
{
    public interface IGraphQueryMarker
    {
        public void SetupQueries(ObjectGraphType graph, IApiApplication api);
    }
}
