using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class RepositoryMasterConfig : IEntityTypeConfiguration<RepositoryMaster>
    {

        public void Configure(EntityTypeBuilder<RepositoryMaster> builder)
        {
            builder.HasKey(x => x.RepoId);
            builder.Property(x => x.RepoId).UseIdentityColumn();
            builder.Property(x => x.RepoName).IsRequired();
            builder.Property(x => x.RepoUrl).IsRequired();
            builder.HasOne(x => x.Owner).WithMany(y => y.Repositories).HasForeignKey(x => x.CreatedBy);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired(false);
        }
    }
}
