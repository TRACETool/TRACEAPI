using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class CommitStoreConfig : IEntityTypeConfiguration<CommitStore>
    {
        public void Configure(EntityTypeBuilder<CommitStore> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.CommitMessage).IsRequired();
            builder.Property(p => p.RepositoryName).IsRequired();
            builder.Property(p => p.TotalChanges).IsRequired();
            builder.Property(p => p.AuthorEmail).IsRequired();
            builder.Property(p => p.AuthorName).IsRequired();
            builder.Property(p => p.CommitDate).IsRequired(false);
            builder.Property(p => p.Deletions).IsRequired(false);
            builder.Property(p => p.Sha).IsRequired(false);

        }
    }
}

