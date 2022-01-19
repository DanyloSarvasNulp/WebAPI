using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Entities.Models;

namespace WebAPI.Entities.Configurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasIndex(a => a.Name)
                .IsUnique();
            builder.Property(a => a.Name);

            
            builder
                .HasMany(a => a.Incidents)
                .WithOne(i => i.Account);
            
            builder
                .HasOne(a => a.Contact)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.ContactId)
                .IsRequired();
        }
    }
}