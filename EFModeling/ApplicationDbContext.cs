using EFModeling.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace EFModeling
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Transaction> Transcations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Building> Buildings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(CardConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(TransactionConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ApartmentImagePathConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(BuildingConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(EnterpriseConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ApartmentConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(AdvertisementConfig).Assembly);
        }

    }
}