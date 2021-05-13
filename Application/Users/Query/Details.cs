using Application.Infrastructure;
using AutoMapper;
using Domain.Entities;
using Domain.GraphObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Query
{
    public class Details
    {
        public class Query : IRequest<GraphUser>
        {
            public string AuthId { get; set; }

            public int Id { get; set; }
        }

        public class Handler : BaseHandler, IRequestHandler<Query, GraphUser>
        {
            private readonly IMediator _mediator;

            public Handler(IServiceProvider provider, IMapper mapper, IConfiguration config, IMediator mediator) : base(provider, mapper, config)
            {
                _mediator = mediator;
            }

            public async Task<GraphUser> Handle(Query request, CancellationToken c)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id || u.AuthId == request.AuthId, c);

                if (user == null)
                    throw new System.Exception("User not found..");

                var mappedUser = _mapper.Map<User, GraphUser>(user);
                mappedUser.ProfilePicture = await _mediator.Send(new ProfilePicture.Query { Id = user.Id }, c);

                return mappedUser;
            }
        }
    }
}
