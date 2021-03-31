using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mappings
{
    public class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Id);

            builder
                .Property(i => i.Id)
                .IsRequired();
            
            builder
                .Property(i => i.Description)
                .IsRequired();
            
            builder
                .Property(i => i.Price)
                .IsRequired();
            
            builder
                .Property(i => i.Active)
                .IsRequired();
        }
    }
}