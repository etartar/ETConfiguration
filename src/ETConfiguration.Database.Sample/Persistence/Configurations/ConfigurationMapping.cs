using ETConfiguration.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETConfiguration.Database.Sample.Persistence.Configurations
{
    public class ConfigurationMapping : IEntityTypeConfiguration<Configuration>
    {
        public void Configure(EntityTypeBuilder<Configuration> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedNever();

            builder.Property(c => c.Section).IsRequired();

            builder.Property(c => c.Key).IsRequired();

            builder.Property(c => c.Value).IsRequired();

            builder.Property(c => c.IsActive).HasDefaultValue(true);
        }
    }
}
