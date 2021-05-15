using Application.Infrastructure;
using Application.Users.Query;
using AutoMapper;
using Domain.GraphObjects;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Application.Users.Command
{
    public class ChangeProfilePicture
    {
        public class Command : GraphImage, IRequest<GraphImage>
        {
            public string AuthId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AuthId).NotEmpty();
                RuleFor(x => x.Data).NotEmpty();
            }
        }

        public class Handler : BaseHandler, IRequestHandler<Command, GraphImage>
        {
            private readonly IMediator _mediator;

            public Handler(IServiceProvider provider, IMapper mapper, IConfiguration config, IMediator mediator) : base(provider, mapper, config) {
                _mediator = mediator;
            }

            public async Task<GraphImage> Handle(Command request, CancellationToken c)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == request.AuthId, c);
                if (user == null)
                    throw new Exception("Failed to find User");

                string b64string = request.Data;

                var converted = Convert.FromBase64String(b64string);
                if (converted.Length <= 0) throw new Exception("Image Data not Base64");

                string storePath = Path.Join(_app.filePath, user.Id.ToString(), "images", "profile.png");

                await File.WriteAllBytesAsync(storePath, converted);

                return await _mediator.Send(new ProfilePicture.Query
                {
                    AuthId = request.AuthId,
                    Id = user.Id
                });
            }
        }
    }
}
