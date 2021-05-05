using AutoMapper;

namespace Domain.Infrastructure.Mapper
{
    public interface ICustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
