﻿using Api.Schemas.Controllers;
using Domain.GraphObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas
{
    public interface IApiApplication
    {
        IGraphUserController Users { get; }
    }

    public class ApiApplication : IApiApplication
    {
        public IGraphUserController Users { get; }

        public ApiApplication(IMediator mediator, IHttpContextAccessor context)
        {
            Users = new GraphUserController(mediator, context);
        }
    }
}
