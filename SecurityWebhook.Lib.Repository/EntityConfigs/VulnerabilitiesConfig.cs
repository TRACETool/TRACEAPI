using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class VulnerabilitiesConfig : IEntityTypeConfiguration<Vulnerabilities>
    {
        public void Configure(EntityTypeBuilder<Vulnerabilities> builder)
        {

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Severity).IsRequired();
            builder.Property(p => p.CurrentHash).IsRequired();
            builder.Property(p => p.PreviousHash).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.DetectionTime).IsRequired();
            builder.Property(p => p.Snapshot).IsRequired(false);
            builder.Property(p => p.Description).IsRequired(false);
            builder.HasOne(x => x.Repository).WithMany(x => x.Vulnerabilities).HasForeignKey(x => x.RepoId);
            builder.HasOne(x => x.Contributor).WithMany(x => x.Vulnerabilities).HasForeignKey(x => x.UserId);










        }
    }
}