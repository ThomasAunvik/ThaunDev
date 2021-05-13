using Application.Infrastructure;
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
            public Handler(IServiceProvider provider, IMapper mapper, IConfiguration config) : base(provider, mapper, config) { }

            public async Task<GraphImage> Handle(Command request, CancellationToken c)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.AuthId == request.AuthId, c);
                if (user == null)
                    throw new Exception("Failed to find User");

                var b64string = request.Data;
                byte[] buffer = new byte[((b64string.Length * 3) + 3) / 4 -
                    (b64string.Length > 0 && b64string[b64string.Length - 1] == '=' ?
                        b64string.Length > 1 && b64string[b64string.Length - 2] == '=' ?
                            2 : 1 : 0)];

                var converted = Convert.TryFromBase64String(b64string, buffer, out int writtenBytes);
                if (!converted) throw new Exception("Image Data not Base64");

                string storePath = Path.Join(_app.filePath, user.Id.ToString(), "images", "profile.png");

                await File.WriteAllBytesAsync(storePath, buffer, c);

                return new GraphImage
                {
                    Name = "ProfilePicture",
                    Data = b64string
                };
            }
        }
    }
}
