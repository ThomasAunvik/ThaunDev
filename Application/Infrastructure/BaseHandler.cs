using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System;

namespace Application.Infrastructure
{
    public class BaseHandler
    {
        protected readonly IServiceProvider _provider;
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _config;

        public BaseHandler(IServiceProvider provider, IMapper mapper, IConfiguration config)
        {
            _provider = provider.CreateScope().ServiceProvider;
            _context = _provider.GetRequiredService<ApplicationDbContext>();
            _mapper = mapper;
            _config = config;
        }
    }
}
