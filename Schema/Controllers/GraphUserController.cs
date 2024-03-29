﻿using Application.Users.Command;
using Application.Users.Query;
using Domain.GraphObjects;
using GraphQL;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Schemas.Controllers
{
    public interface IGraphUserController
    {
        ConcurrentStack<GraphUser> AllUsers { get; }
        Task<GraphUser> GetCurrentUser();
        Task<GraphUser> GetUser(int id);
        GraphUser AddUser(GraphUser user);
        Task<GraphUser> EditUser(int id, GraphUser user);
        Task<GraphImage> EditProfilePicture(int id, string image);
    }

    public class GraphUserController : GraphBaseController, IGraphUserController
    {

        public GraphUserController(IMediator mediator, IHttpContextAccessor context) : base(mediator, context)
        {

        }

        public ConcurrentStack<GraphUser> AllUsers => new(new List<GraphUser>() { new GraphUser() { Id = 1, Username = "Thaun_", FirstName = "Thomas", LastName = "Aunvik" } });

        public async Task<GraphUser> GetCurrentUser()
        {
            return await _mediator.Send(new Details.Query { AuthId = AuthId });
        }

        public async Task<GraphUser> GetUser(int id)
        {
            return await _mediator.Send(new Details.Query { Id = id });
        }

        public GraphUser AddUser(GraphUser user)
        {
            return new GraphUser() { Id = 1, FirstName = "Thomas", LastName = "Aunvik" };
        }

        public async Task<GraphUser> EditUser(int id, GraphUser user)
        {
            return await _mediator.Send(new Edit.Command { Id = id, AuthId = AuthId, Roles = UserRoles, User = user });
        }

        public async Task<GraphImage> EditProfilePicture(int id, string image)
        {
            return await _mediator.Send(new ChangeProfilePicture.Command { AuthId = AuthId, Data = image });
        }
    }
}
