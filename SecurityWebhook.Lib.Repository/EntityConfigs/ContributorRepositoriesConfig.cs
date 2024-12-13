using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class ContributorRepositoriesConfig : IEntityTypeConfiguration<ContributorRepositories>
    {
        public void Configure(EntityTypeBuilder<ContributorRepositories> builder)
        {

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.HasOne(x => x.Repository).WithMany(x => x.ContributorRepositories).HasForeignKey(x => x.RepositoryId);
            builder.HasOne(x => x.Contributor).WithMany(x => x.ContributorRepositories).HasForeignKey(x => x.ContributorId);

        }
    }
}
