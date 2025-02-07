using Microsoft.EntityFrameworkCore;
using ProductsAPI.Mappings;

namespace ProductsAPI.Contexts
{
    /// <summary>
    /// Classe de contexto para conexão no banco de dados
    /// e para configuração do Entity Framework
    /// </summary>
    public class DataContext : DbContext
    {
        //configurando a conexão do banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDProductsAPI;Integrated Security=True;");
        }

        //método para adicionar as classes de mapeamento do projeto
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}
