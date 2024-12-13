using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class ContributorMasterConfig : IEntityTypeConfiguration<ContributorMaster>
    {

        public void Configure(EntityTypeBuilder<ContributorMaster> builder)
        {
            builder.HasKey(x => x.ContributorId);
            builder.Property(x => x.ContributorId).IsRequired().UseIdentityColumn();
            builder.Property(x => x.ContributorName).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Password).IsRequired(false);
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.ContributorEmail).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
        }
    }
}
    
