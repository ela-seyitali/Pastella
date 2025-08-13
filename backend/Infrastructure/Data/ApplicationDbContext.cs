using Microsoft.EntityFrameworkCore;
using Pastella.Backend.Core.Entities;

namespace Pastella.Backend.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Existing DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<SweetDesign> SweetDesigns { get; set; }
        public DbSet<SweetDecoration> SweetDecorations { get; set; }
        public DbSet<Decoration> Decorations { get; set; }
        public DbSet<DesignImage> DesignImages { get; set; }

        // New DbSets for Lola-like features
        public DbSet<Bakery> Bakeries { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CakeCustomization> CakeCustomizations { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<DeviceToken> DeviceTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);

            // SweetDecoration composite primary key (eğer Id kullanmak istemiyorsanız)
            // modelBuilder.Entity<SweetDecoration>()
            //     .HasKey(sd => new { sd.SweetDesignId, sd.DecorationId });

            // SweetDecoration relationships
            modelBuilder.Entity<SweetDecoration>()
                .HasOne(sd => sd.SweetDesign)
                .WithMany(s => s.SweetDecorations)
                .HasForeignKey(sd => sd.SweetDesignId);

            modelBuilder.Entity<SweetDecoration>()
                .HasOne(sd => sd.Decoration)
                .WithMany()
                .HasForeignKey(sd => sd.DecorationId);

            // SweetDesign relationships - sadece CreatedByUser ilişkisi
            modelBuilder.Entity<SweetDesign>()
                .HasOne(sd => sd.CreatedByUser)
                .WithMany()
                .HasForeignKey(sd => sd.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // SweetDesign decimal precision
            modelBuilder.Entity<SweetDesign>()
                .Property(sd => sd.Price)
                .HasPrecision(18, 2);

            // Decoration decimal precision
            modelBuilder.Entity<Decoration>()
                .Property(d => d.Price)
                .HasPrecision(18, 2);

            // Order relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cake)
                .WithMany()
                .HasForeignKey(o => o.CakeId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Bakery)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BakeryId)
                .IsRequired(false);

            // Cake relationships
            modelBuilder.Entity<Cake>()
                .HasMany(c => c.Comments)
                .WithOne(co => co.Cake)
                .HasForeignKey(co => co.CakeId);

            // CakeCustomization relationship
            modelBuilder.Entity<CakeCustomization>()
                .HasOne(cc => cc.Order)
                .WithOne()
                .HasForeignKey<CakeCustomization>(cc => cc.OrderId);

            // DeliveryAddress relationship
            modelBuilder.Entity<DeliveryAddress>()
                .HasOne(da => da.User)
                .WithMany()
                .HasForeignKey(da => da.UserId);

            // Decimal precision for prices
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cake>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CakeCustomization>()
                .Property(cc => cc.BasePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CakeCustomization>()
                .Property(cc => cc.CustomizationPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CakeCustomization>()
                .Property(cc => cc.TotalPrice)
                .HasPrecision(18, 2);
        }
    }
}