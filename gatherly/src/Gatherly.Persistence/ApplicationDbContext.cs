using Gatherly.Domain.Entities;
using Gatherly.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence;

public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(builder =>
        {
            builder.HasKey(m => m.Id);

            builder
                .Property(m => m.Email)
                .HasMaxLength(255)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value).Value)
                .HasColumnName("Email");

            builder
                .HasIndex(m => m.Email)
                .IsUnique();

            builder
                .Property(m => m.FirstName)
                .HasMaxLength(100)
                .HasConversion(
                    firstName => firstName.Value,
                    value => FirstName.Create(value).Value)
                .HasColumnName("FirstName");

            builder
                .Property(m => m.LastName)
                .HasMaxLength(100)
                .HasConversion(
                    lastName => lastName.Value,
                    value => LastName.Create(value).Value)
                .HasColumnName("LastName");

            var memberId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            builder.HasData(Member.Create(
                memberId,
                Email.Create("jack.daniels@mail.com").Value,
                FirstName.Create("Jack").Value,
                LastName.Create("Daniels").Value
            ));
        });
    }
}