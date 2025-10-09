﻿using Backend.Common;
using Backend.Entities;
using Backend.Features.Users._Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence;

public static class ModelBuilderExtensions
{
    public static void ApplyCommonConfigureAcrossEntity(this ModelBuilder builder)
    {
        var allEntities = builder.Model.GetEntityTypes().ToList();

        var tenantEntities = allEntities
            .Where(e => typeof(ITenant).IsAssignableFrom(e.ClrType));
        foreach (var entityType in tenantEntities)
        {
            builder.Entity(entityType.ClrType).Property("TenantId").IsRequired();
            builder.Entity(entityType.ClrType).Property("TenantId").HasMaxLength(36);
        }
        var mutableEntityTypes = allEntities
            .Where(e => typeof(ICommonEntityProperty).IsAssignableFrom(e.ClrType));
        foreach (var entityType in mutableEntityTypes)
        {
            builder.Entity(entityType.ClrType).Property("Id").HasMaxLength(36);
            builder.Entity(entityType.ClrType).Property("CreatedById").HasMaxLength(36);
        }
        var temporalEntities = allEntities
            .Where(e => typeof(IHistory).IsAssignableFrom(e.ClrType));
        foreach (var entityType in temporalEntities)
        {
            builder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name, b => b.IsTemporal());

            if (DatabaseSelected.Type == DatabaseType.Postgresql)
            {
                builder.Entity(entityType.ClrType).Property("CreatedOn")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
            else if (DatabaseSelected.Type == DatabaseType.MySql)
            {
                builder.Entity(entityType.ClrType).Property("CreatedOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
            else
            {
                throw new NotSupportedException($"Database type {DatabaseSelected.Type} is not supported for temporal entities.");
            }
        }

        var commonEntityType = typeof(ICommonAuthEntityProperty);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (commonEntityType.IsAssignableFrom(entityType.ClrType) && entityType.ClrType != commonEntityType)
            {
                builder.Entity(entityType.ClrType).HasOne(typeof(KrafterUser), "CreatedBy")
                    .WithMany()
                    .HasForeignKey("CreatedById")
                    .OnDelete(DeleteBehavior.Restrict);

                builder.Entity(entityType.ClrType).HasOne(typeof(KrafterUser), "UpdatedBy")
                    .WithMany()
                    .HasForeignKey("UpdatedById")
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}