using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramBridge.Domain.Entities;

namespace TelegramBridge.Infrastructure.Persistence.Configuration;

public class WebhookSubsctiptionConfiguration : IEntityTypeConfiguration<WebhookSubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<WebhookSubscriptionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(512)
            .HasColumnName("name");
        
        builder.Property(x => x.Url)
            .HasMaxLength(512)
            .HasColumnName("url")
            .IsRequired();

        builder.Property(x => x.Event)
            .HasMaxLength(32)
            .HasColumnName("event")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired()
            .HasDefaultValueSql("now() at time zone 'utc'");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("now() at time zone 'utc'");
    }
}