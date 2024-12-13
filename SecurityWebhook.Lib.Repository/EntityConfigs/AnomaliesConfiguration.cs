using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class AnomaliesConfiguration : IEntityTypeConfiguration<Anomalies>
    {
        public void Configure(EntityTypeBuilder<Anomalies> builder) 
        {
            builder.HasKey(p => p.AnomaliesId);
            builder.Property(p=> p.AnomaliesId).UseIdentityColumn();
            builder.Property(p => p.AnomalyType).IsRequired();
            builder.Property(p => p.Severity).IsRequired();
            builder.Property(p => p.CurrentHash).IsRequired();
            builder.Property(p => p.PreviousHash).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Description).IsRequired(false);
            builder.Property(p => p.ActionTaken).IsRequired(false);
            builder.Property(p => p.CreatedOn).IsRequired(false);
            builder.HasOne(x => x.Repository).WithMany(x => x.Anomalies).HasForeignKey(x => x.RepoId);
            builder.HasOne(x => x.Contributor).WithMany(x => x.Anomalies).HasForeignKey(x => x.UserId);

        }
    }
}
