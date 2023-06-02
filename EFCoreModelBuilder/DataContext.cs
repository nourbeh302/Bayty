using EFCoreModelBuilder.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace EFCoreModelBuilder
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Transaction> Transcations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<HouseBase> HouseBases { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Building> Buildings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(CardConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(TransactionConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(HouseBaseImagePathConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(BuildingConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(EnterpriseConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(HouseBaseConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(AdvertisementConfig).Assembly);
        }

    }
}