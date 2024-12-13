using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class RoleMasterConfig : IEntityTypeConfiguration<RoleMaster>
    {
        public void Configure(EntityTypeBuilder<RoleMaster> builder)
        {

            builder.HasKey(p => p.RoleId);
            builder.Property(p => p.RoleId).UseIdentityColumn();
            builder.Property(p => p.RoleDescription).IsRequired();
            builder.Property(p => p.RoleName).IsRequired();

        }
    }
}







