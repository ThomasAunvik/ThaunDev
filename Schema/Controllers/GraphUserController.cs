using Application.Users.Query;
using Domain.GraphObjects;
using GraphQL;
using MediatR;
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
    }

    public class GraphUserController : GraphBaseController, IGraphUserController
    {

        public GraphUserController(IMediator mediator) : base(mediator)
        {

        }

        public ConcurrentStack<GraphUser> AllUsers => new(new List<GraphUser>() { new GraphUser() { Id = 1, Username = "Thaun_", FirstName = "Thomas", LastName = "Aunvik" } });

        public async Task<GraphUser> GetCurrentUser()
        {
            return await _mediator.Send(new Details.Query { Id = 1 });
        }

        public async Task<GraphUser> GetUser(int id)
        {
            return await _mediator.Send(new Details.Query { Id = id });
        }

        public GraphUser AddUser(GraphUser user)
        {
            return new GraphUser() { Id = 1, FirstName = "Thomas", LastName = "Aunvik" };
        }
    }
}
