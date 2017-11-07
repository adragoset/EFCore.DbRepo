using Microsoft.EntityFrameworkCore;

namespace EFCore.DbRepo.Tests.ConcreteImplementations
{
    public class TestRecordContext : DbContext
    {
        public DbSet<TestRecord> UserPrincipleRecords { get; set; }

        public TestRecordContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            var userPrinciple = modelBuilder.Entity<TestRecord>().ToTable("TestRecords");
            userPrinciple.Property(up => up.Id).HasDefaultValueSql("uuid_generate_v4()");
            userPrinciple.Property(up => up.Name).IsRequired();
            userPrinciple.HasKey(b => b.Id);
            userPrinciple.HasIndex(up => up.Name);
        }
    }
}