﻿using Backend.Features.Tenants;
using Backend.Features.Users._Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence;

public class TenantDbContext(DbContextOptions<TenantDbContext> options)
    : DbContext(options)
{
    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasQueryFilter(c => c.IsDeleted == false);

            // Configure CreatedOn with database-specific settings
         //   ConfigureCreatedOnColumn(entity);

            // Remove IsTemporal() for cross-database compatibility
            entity.ToTable(nameof(Tenant));

            entity.HasData(new List<Tenant>()
            {
                new Tenant()
                {
                    Id = KrafterInitialConstants.RootTenant.Id,
                    Identifier = KrafterInitialConstants.RootTenant.Identifier,
                    IsActive = true,
                    Name = KrafterInitialConstants.RootTenant.Name,
                    CreatedOn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),  // ✅ FIXED
                    ValidUpto = DateTime.MaxValue,
                    AdminEmail = KrafterInitialConstants.RootUser.EmailAddress,
                }
            });
        });
    }

    private void ConfigureCreatedOnColumn(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tenant> entity)
    {
        var provider = Database.ProviderName;

        switch (provider)
        {
            case "Microsoft.EntityFrameworkCore.SqlServer":
                entity.Property(b => b.CreatedOn)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETUTCDATE()");
                break;

            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                entity.Property(b => b.CreatedOn)
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("NOW()") // NOW() returns timestamptz
                    .ValueGeneratedOnAdd();
                break;

            case "Pomelo.EntityFrameworkCore.MySql":
            case "MySql.EntityFrameworkCore":
                entity.Property(b => b.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                break;

            case "Microsoft.EntityFrameworkCore.Sqlite":
                entity.Property(b => b.CreatedOn)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                break;

            default:
                // Fallback: set in application code
                entity.Property(b => b.CreatedOn)
                    .HasDefaultValue(DateTime.UtcNow);
                break;
        }
    }
}