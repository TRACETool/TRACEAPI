using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Repository.Entities;
using SecurityWebhook.Lib.Repository.EntityConfigs;

namespace SecurityWebhook.Lib.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContributorMasterConfig());
            modelBuilder.ApplyConfiguration(new RepositoryMasterConfig());
            modelBuilder.ApplyConfiguration(new ScanDetailsConfig());
            modelBuilder.ApplyConfiguration(new VulnerabilitiesConfig());
            modelBuilder.ApplyConfiguration(new AnomaliesConfiguration());
            modelBuilder.ApplyConfiguration(new ContributorRepositoriesConfig());
            modelBuilder.ApplyConfiguration(new RoleMasterConfig());
            modelBuilder.ApplyConfiguration(new ScanFrequencyMasterConfig());
            modelBuilder.ApplyConfiguration(new RepoScanMetadataConfig());
        }

        public DbSet<ImmutableServiceLogs> ImmutableServiceLogs { get; set; }
        public DbSet<ContributorMaster> ContributorMaster { get; set; }
        public DbSet<RepositoryMaster> RepositoryMaster { get; set; }   
        public DbSet<RoleMaster> RoleMaster { get; set; }
        public DbSet<ScanFrequencyMaster> ScanFrequencyMaster { get; set; }
        public DbSet<ScanDetails> ScanDetails { get; set; }
        public DbSet<Vulnerabilities> Vulnerabilities { get; set; }
        public DbSet<Anomalies> Anomalies { get; set; }
        public DbSet<ContributorRepositories> ContributorRepositories { get; set; }
        public DbSet<RepoScanMetadata> RepoScanMetadata { get; set; }
        public DbSet<CommitStore> CommitStore { get; set; }
    }
}
