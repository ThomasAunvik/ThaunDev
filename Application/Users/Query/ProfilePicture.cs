using Application.Infrastructure;
using AutoMapper;
using Domain.Entities;
using Domain.GraphObjects;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Query
{
    public class ProfilePicture
    {
        public class Query : IRequest<GraphImage>
        {
            public string AuthId { get; set; }
            public int Id { get; set; }
        }

        public class Handler : BaseHandler, IRequestHandler<Query, GraphImage>
        {
            public Handler(IServiceProvider provider, IMapper mapper, IConfiguration config) : base(provider, mapper, config)
            {
            }

            public async Task<GraphImage> Handle(Query request, CancellationToken c)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, c);

                if (user == null)
                    throw new System.Exception("User not found..");

                string storePath = Path.Join(_app.filePath, user.Id.ToString(), "images", "profile.png");
                if (!File.Exists(storePath))
                {
                    return new GraphImage
                    {
                        Name = "DefaultProfilePicture",
                        Data = ""
                    };
                }

                byte[] pureData = await File.ReadAllBytesAsync(storePath, c);

                string base64 = Convert.ToBase64String(pureData);

                var contentType = GetContentType(storePath);
                var imageData = "data:" + contentType + ";base64," + base64;

                return new GraphImage
                {
                    Name = "ProfilePicture",
                    Data = imageData
                };
            }
            private static string GetContentType(string filePath)
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out string contentType))
                {
                    contentType = "application/octet-stream";
                }
                return contentType;
            }
        }
    }
}
