using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class RepoScanMetadataConfig : IEntityTypeConfiguration<RepoScanMetadata>
    {
        public void Configure(EntityTypeBuilder<RepoScanMetadata> builder) 
        { 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Url).IsRequired();
            builder.Property(x => x.ProjectKey).IsRequired();
            builder.Property(x => x.Path).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.HasOne(x => x.RepositoryMaster).WithOne(x => x.Metadata).HasForeignKey<RepoScanMetadata>(x => x.RepoId);
        
        }
    }
}
