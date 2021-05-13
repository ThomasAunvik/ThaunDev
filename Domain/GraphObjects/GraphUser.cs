using AutoMapper;
using Domain.Entities;
using Domain.Infrastructure.Mapper;

namespace Domain.GraphObjects
{
    public class GraphUser : ICustomMapping
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public GraphImage ProfilePicture { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, GraphUser>();
        }
    }
}
