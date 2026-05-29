using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
{
    public void Configure(EntityTypeBuilder<SaleOrder> builder)
    {
        builder.ToTable("SaleOrders");
        
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(o => o.CustomerName).IsRequired().HasMaxLength(50);
        builder.Property(o => o.BranchName).IsRequired().HasMaxLength(50);
        builder.Property(o => o.CancelStatus).IsRequired();
    }
}
