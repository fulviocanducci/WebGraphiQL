using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
namespace DataAccess.Maps
{
  public class PeopleMap : IEntityTypeConfiguration<People>
  {
    public void Configure(EntityTypeBuilder<People> builder)
    {
      builder.ToTable("Peoples");
      builder.HasKey(x => x.Id)
        .HasName("Id");
      builder.Property(x => x.Id)
        .HasColumnName("Id")
        .UseIdentityColumn();
      builder.Property(x => x.Name)
        .HasColumnName("Name")
        .HasMaxLength(100)
        .IsUnicode(false)
        .IsRequired();
      builder.Property(x => x.Created)
        .HasColumnName("Created")
        .IsRequired();
      builder.Property(x => x.Active)
        .HasColumnName("Active");
    }
  }
}
