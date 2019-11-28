using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DataAccess.Maps
{
  public class ItemsMap : IEntityTypeConfiguration<Items>
  {
    public void Configure(EntityTypeBuilder<Items> builder)
    {
      builder.ToTable("Items");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
      builder.Property(x => x.Title).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnName("Title");
      builder.Property(x => x.Updated).HasColumnName("Updated");
    }
  }
}
