using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class AirleganceDbContext : DbContext
{
    public AirleganceDbContext(DbContextOptions<AirleganceDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Flight> Flights { get; set; } = null!;
    public DbSet<FlightSchedule> FlightSchedule { get; set; } = null!;
    public DbSet<UserToken> UserTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.Password).IsRequired();

            entity.HasOne(u => u.UserToken)
                .WithOne(t => t.User)
                .HasForeignKey<UserToken>(t => t.UserId);

            entity.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersRoles",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"));
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            entity.Property(e => e.UserId).IsRequired();
            entity.HasIndex(e => e.UserId).IsUnique();

            entity.Property(e => e.Token).IsRequired();
            entity.HasIndex(e => e.Token).IsUnique();

            entity.HasOne(t => t.User)
                .WithOne(u => u.UserToken)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            entity.Property(e => e.Country).IsRequired();
            entity.Property(e => e.City).IsRequired();
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            entity.HasOne(f => f.SourceLocation)
                .WithMany()
                .HasForeignKey(f => f.SourceLocationId);

            entity.HasOne(f => f.DestinationLocation)
                .WithMany()
                .HasForeignKey(f => f.DestinationLocationId);

            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.Rating).IsRequired();
        });

        modelBuilder.Entity<FlightSchedule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            entity.HasOne(fs => fs.Flight)
                .WithMany()
                .HasForeignKey(fs => fs.FlightId);

            entity.HasOne(fs => fs.User)
                .WithMany(u => u.FlightSchedules)
                .HasForeignKey(fs => fs.UserId);

            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
        });

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.SourceLocation)
            .WithMany(l => l.SourceFlights)
            .HasForeignKey(f => f.SourceLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.DestinationLocation)
            .WithMany(l => l.DestinationFlights)
            .HasForeignKey(f => f.DestinationLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FlightSchedule>()
            .HasOne(fs => fs.User)
            .WithMany(u => u.FlightSchedules)
            .HasForeignKey(fs => fs.UserId);


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}