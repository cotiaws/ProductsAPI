using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsAPI.Entities;

namespace ProductsAPI.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Produto.
    /// </summary>
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCTS"); //nome da tabela no banco de dados

            builder.HasKey(p => p.Id); //chave primária da tabela

            builder.Property(p => p.Id)
                .HasColumnName("ID"); //nome da coluna no banco de dados

            builder.Property(p => p.Name)
                .HasColumnName("NAME") //nome da coluna no banco de dados
                .HasMaxLength(100) //tamanho máximo de caracteres
                .IsRequired(); //campo obrigatório (not null)

            builder.Property(p => p.Price)
                .HasColumnName("PRICE") //nome da coluna no banco de dados
                .HasColumnType("decimal(10,2)") //tamanho do campo decimal
                .IsRequired(); //campo obrigatório (not null)

            builder.Property(p => p.Quantity)
                .HasColumnName("QUANTITY") //nome da coluna no banco de dados
                .IsRequired(); //campo obrigatório (not null)

            builder.Property(p => p.CategoryId)
                .HasColumnName("CATEGORY_ID") //nome da coluna no banco de dados
                .IsRequired(); //campo obrigatório (not null)

            builder.HasOne(p => p.Category) //Produto TEM 1 Categoria
                .WithMany(c => c.Products) //Categoria TEM MUITOS Produtos
                .HasForeignKey(p => p.CategoryId); //Chave estrangeira do relacionamento
        }
    }
}
