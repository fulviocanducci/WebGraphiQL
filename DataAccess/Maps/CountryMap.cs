using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
namespace DataAccess.Maps
{
  public class CountryMap : IEntityTypeConfiguration<Country>
  {
    public void Configure(EntityTypeBuilder<Country> builder)
    {
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Id)
        .HasColumnName("Id")
        .UseIdentityColumn();

      builder.Property(x => x.Name)
        .IsRequired()
        .IsUnicode(false)
        .HasMaxLength(150)
        .HasColumnName("Name");

      builder.Property(x => x.StateId)
        .IsRequired()
        .IsUnicode()
        .HasColumnName("StateId");

      builder.HasOne(x => x.State)
        .WithMany(x => x.Country)
        .HasForeignKey(x => x.StateId);
    }
  }
}
