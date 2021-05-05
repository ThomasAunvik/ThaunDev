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
            public int Id { get; set; }
        }

        public class Handler : BaseHandler, IRequestHandler<Query, GraphUser>
        {
            public Handler(IServiceProvider provider, IMapper mapper, IConfiguration config) : base(provider, mapper, config)
            {
            }

            public async Task<GraphUser> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id);

                if (user == null)
                    throw new System.Exception("User not found..");

                return _mapper.Map<User, GraphUser>(user);
            }
        }
    }
}
