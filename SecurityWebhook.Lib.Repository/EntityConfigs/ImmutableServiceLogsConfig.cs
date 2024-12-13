using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityWebhook.Lib.Repository.Entities;

namespace SecurityWebhook.Lib.Repository.EntityConfigs
{
    public class ImmutableServiceLogsConfig : IEntityTypeConfiguration<ImmutableServiceLogs>
    {
        public void Configure(EntityTypeBuilder<ImmutableServiceLogs> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.User).IsRequired();
            builder.Property(x => x.Repository).IsRequired();
            builder.Property(x => x.Owner).IsRequired();
            builder.Property(x => x.PreviousHash).IsRequired(false);
            builder.Property(x => x.CurrentHash).IsRequired();
            builder.Property(x => x.Action).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Timestamp).IsRequired();
        }
    }
}
