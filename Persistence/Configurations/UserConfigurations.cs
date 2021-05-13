using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.Property(e => e.Id).HasColumnName("Id").HasColumnType("integer");
            entity.Property(e => e.FirstName).HasColumnName("FirstName").HasColumnType("text");
            entity.Property(e => e.LastName).HasColumnName("LastName").HasColumnType("text");
            entity.Property(e => e.AuthId).HasColumnName("AuthId").HasColumnType("text");
        }
    }
}
