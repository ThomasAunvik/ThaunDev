using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System;
using System.Security.Claims;

namespace Application.Infrastructure
{
    public class BaseHandler
    {
        protected readonly IServiceProvider _provider;
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _config;
        protected readonly ApplicationConfig _app;

        public BaseHandler(IServiceProvider provider, IMapper mapper, IConfiguration config)
        {
            _provider = provider.CreateScope().ServiceProvider;
            _context = _provider.GetRequiredService<ApplicationDbContext>();
            _mapper = mapper;
            _config = config;
            
            _app = new ApplicationConfig()
            {
                filePath = config.GetSection("Config").GetSection("filePath").Value
            };
        }
    }
}
