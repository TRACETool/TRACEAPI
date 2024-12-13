using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class ScanFrequencyMasterConfig : IEntityTypeConfiguration<ScanFrequencyMaster>
    {
        public void Configure(EntityTypeBuilder<ScanFrequencyMaster> builder)
        {

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Events).IsRequired();
            builder.HasOne(x => x.Repository).WithMany(x => x.ScanFrequencyMaster).HasForeignKey(x => x.RepoId);


        }
    }
}


