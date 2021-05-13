using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas.Controllers
{
    public class GraphBaseController
    {
        protected readonly IMediator _mediator;
        protected readonly IHttpContextAccessor _context;

        protected ClaimsPrincipal UserPrincipal => _context.HttpContext.User;
        protected string AuthId => UserPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        protected List<string> UserRoles => UserPrincipal.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();

        public GraphBaseController(IMediator mediator, IHttpContextAccessor context)
        {
            _mediator = mediator;
            _context = context;
        }
    }
}
