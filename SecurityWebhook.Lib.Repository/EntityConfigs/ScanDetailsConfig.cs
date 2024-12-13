using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class ScanDetailsConfig : IEntityTypeConfiguration<ScanDetails>
    {
        public void Configure(EntityTypeBuilder<ScanDetails> builder) 
        {
        
            builder.HasKey(p =>p.ScanId);
            builder.Property(p => p.ScanId).UseIdentityColumn();
            builder.Property(p => p.Event).IsRequired();
            builder.Property(p => p.CurrentHash).IsRequired();
            builder.Property(p => p.PreviousHash).IsRequired();
            builder.Property(p => p.TotalVulnerabilities).IsRequired();
            builder.Property(p => p.WhiteMarked).IsRequired();
            builder.Property(p => p.YellowMarked).IsRequired();
            builder.Property(p => p.RedMarked).IsRequired();
            builder.Property(p => p.RawData).IsRequired();
            builder.Property(p => p.ScanTime).IsRequired();
            builder.Property(p => p.TimeElapsed).IsRequired();
            builder.HasOne(x => x.Repository).WithMany(x => x.ScanDetails).HasForeignKey(x => x.RepoId);
            builder.HasOne(x => x.Contributor).WithMany(x => x.ScanDetails).HasForeignKey(x => x.UserId);










        }
    }
}
