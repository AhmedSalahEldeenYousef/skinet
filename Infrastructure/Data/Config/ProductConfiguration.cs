using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // {{p}} stands For Product Entity
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();

            //then We Have To Configuar The RelationShip Betwen ProductType && ProductTypeId And ProductBrand && ProductBrandId
            //b => Stands For ProductBrand Entity

            builder.HasOne(b => b.ProductBrand).WithMany()
            .HasForeignKey(p => p.ProductBrandId);

            //t => stand For ProductType
            builder.HasOne(t => t.ProductType).WithMany()
           .HasForeignKey(p => p.ProductTypeId);


        }
    }
}