using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Entities.Models;

namespace WebAPI.Entities.Configurations
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.FirstName)
                .IsRequired();

            builder
                .Property(c => c.LastName)
                .IsRequired();

            builder
                .Property(c => c.Email)
                .IsRequired();

            
            builder
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Contact);
        }
    }
}