using dotnet_rpg.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_rpg.Infrastructure.Persistence.Configurations;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id).IsUnique();
        builder.Property(c => c.Id).HasDefaultValue(Guid.NewGuid());
        builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Class).IsRequired();
        builder.Property(c => c.Defense).HasDefaultValue(1);
        builder.Property(c => c.Intelligence).HasDefaultValue(1);
        builder.Property(c => c.Strength).HasDefaultValue(1);
    }
}