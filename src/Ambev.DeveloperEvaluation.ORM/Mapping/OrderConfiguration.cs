using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(o => o.CustomerId).IsRequired();
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Customer)
                   .WithMany()
                   .HasForeignKey(oi => oi.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(oi => oi.Branch)
                   .WithMany()
                   .HasForeignKey(oi => oi.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();
        }
    }
}