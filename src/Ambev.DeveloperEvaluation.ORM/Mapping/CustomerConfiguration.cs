using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Document)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Phone)
                   .HasMaxLength(20);
        }
    }
}