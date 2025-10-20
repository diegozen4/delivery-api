
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Authentication
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    // Commerce
    public DbSet<Commerce> Commerces { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    // Ordering
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    // Shared
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Image> Images { get; set; }

    // Delivery
    public DbSet<DeliveryUser> DeliveryUsers { get; set; }
    public DbSet<DeliveryCandidate> DeliveryCandidates { get; set; }
    public DbSet<DeliveryGroup> DeliveryGroups { get; set; }
    public DbSet<DeliveryGroupUser> DeliveryGroupUsers { get; set; }

    // Payments
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Authentication Schema
        modelBuilder.Entity<User>().ToTable("Users", "auth");
        modelBuilder.Entity<Role>().ToTable("Roles", "auth");
        modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens", "auth");

        // Commerce Schema
        modelBuilder.Entity<Commerce>().ToTable("Commerces", "commerce");
        modelBuilder.Entity<Category>().ToTable("Categories", "commerce");
        modelBuilder.Entity<Product>().ToTable("Products", "commerce");

        // Ordering Schema
        modelBuilder.Entity<Order>().ToTable("Orders", "order");
        modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails", "order");
        modelBuilder.Entity<OrderStatusHistory>().ToTable("OrderStatusHistories", "order");

        // Shared Schema
        modelBuilder.Entity<Address>().ToTable("Addresses", "shared");
        modelBuilder.Entity<Notification>().ToTable("Notifications", "shared");
        modelBuilder.Entity<Image>().ToTable("Images", "shared");

        // Delivery Schema
        modelBuilder.Entity<DeliveryUser>().ToTable("DeliveryUsers", "delivery");
        modelBuilder.Entity<DeliveryCandidate>().ToTable("DeliveryCandidates", "delivery");
        modelBuilder.Entity<DeliveryGroup>().ToTable("DeliveryGroups", "delivery");
        modelBuilder.Entity<DeliveryGroupUser>().ToTable("DeliveryGroupUsers", "delivery");

        // Payments Schema
        modelBuilder.Entity<Transaction>().ToTable("Transactions", "payments");

        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        // User and Role (Many-to-Many) is now managed by ASP.NET Core Identity

        // Order and OrderDetail (One-to-Many)
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId);
            
        // DeliveryGroup and DeliveryUser (Many-to-Many)
        modelBuilder.Entity<DeliveryGroupUser>()
            .HasKey(dgu => new { dgu.DeliveryGroupId, dgu.DeliveryUserId });

        modelBuilder.Entity<DeliveryGroupUser>()
            .HasOne(dgu => dgu.DeliveryGroup)
            .WithMany(dg => dg.DeliveryGroupUsers)
            .HasForeignKey(dgu => dgu.DeliveryGroupId);

        modelBuilder.Entity<DeliveryGroupUser>()
            .HasOne(dgu => dgu.DeliveryUser)
            .WithMany(du => du.DeliveryGroups)
            .HasForeignKey(dgu => dgu.DeliveryUserId);
            
        // Enum Conversions
        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();
            
        modelBuilder.Entity<OrderStatusHistory>()
            .Property(osh => osh.Status)
            .HasConversion<string>();

        modelBuilder.Entity<DeliveryUser>()
            .Property(du => du.Status)
            .HasConversion<string>();
    }
}
