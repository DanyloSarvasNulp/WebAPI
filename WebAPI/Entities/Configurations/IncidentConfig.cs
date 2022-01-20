using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Entities.Models;

namespace WebAPI.Entities.Configurations
{
    public class IncidentConfig : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            // builder.HasKey(i => i.Name);
            // builder
            //     .Property(i => i.Name)
            //     .HasDefaultValueSql("newsequentialid()");

            builder
                .Property(i => i.Name)
                .HasDefaultValueSql("NEWID()");    
            
            builder
                .Property(i => i.Description)
                .IsRequired();

            
            builder
                .HasOne(i => i.Account)
                .WithMany(a => a.Incidents)
                .HasForeignKey(i => i.AccountId)
                .IsRequired();
        }
    }
}