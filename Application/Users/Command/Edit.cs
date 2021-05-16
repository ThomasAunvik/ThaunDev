using Application.Infrastructure;
using Application.Users.Query;
using AutoMapper;
using Domain.GraphObjects;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Command
{
    public class Edit
    {
        public class Command : IRequest<GraphUser>
        {
            public int Id { get; set; }
            public string AuthId { get; set; }
            public List<string> Roles { get; set; }
            public GraphUser User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

            }
        }

        public class Handler : BaseHandler, IRequestHandler<Command, GraphUser>
        {
            private readonly IMediator _mediator;

            public Handler(IServiceProvider provider, IMapper mapper, IConfiguration config, IMediator mediator) : base(provider, mapper, config) {
                _mediator = mediator;
            }

            public async Task<GraphUser> Handle(Command request, CancellationToken c)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, c);
                if (user == null)
                    throw new Exception("Failed to find User");

                if (user.AuthId != request.AuthId && !request.Roles.Contains("Admin"))
                    throw new Exception("You don't have permission to edit this user");

                if(!string.IsNullOrWhiteSpace(request.User.Username)) user.Username = request.User.Username;
                if (!string.IsNullOrWhiteSpace(request.User.FirstName)) user.FirstName = request.User.FirstName;
                if (!string.IsNullOrWhiteSpace(request.User.LastName)) user.LastName = request.User.LastName;
                if (!string.IsNullOrWhiteSpace(request.User.Email))
                {
                    var isValid = MailAddress.TryCreate(request.User.Email, out MailAddress mail);
                    if (!isValid) throw new Exception("Email is invalid");

                    user.Email = mail.Address;
                }

                await _context.SaveChangesAsync(c);

                return await _mediator.Send(new Details.Query { AuthId = request.AuthId, Id = request.Id }, c);
            }
        }
    }
}
