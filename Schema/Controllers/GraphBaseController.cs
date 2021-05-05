using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas.Controllers
{
    public class GraphBaseController
    {
        protected readonly IMediator _mediator;

        public GraphBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
