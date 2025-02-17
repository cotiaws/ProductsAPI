using Microsoft.EntityFrameworkCore;
using ProductsAPI.Contexts;
using ProductsAPI.DTOs;
using ProductsAPI.Entities;

namespace ProductsAPI.Repositories
{
    public class ProductRepository
    {
        //método para gravar um produto no banco de dados
        public void Add(Product product)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(product);
                dataContext.SaveChanges();
            }
        }

        //método para atualizar um produto no banco de dados
        public void Update(Product product)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(product);
                dataContext.SaveChanges();
            }
        }

        //método para excluir um produto no banco de dados
        public void Delete(Product product)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Remove(product);
                dataContext.SaveChanges();
            }
        }

        //método para consultar todos os produtos cadastrados
        public List<Product> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Product>()
                    .Include(p => p.Category) //JOIN
                    .OrderBy(p => p.Name)
                    .ToList();
            }
        }

        //método para consultar 1 produto através do ID
        public Product? GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Product>()
                    .Find(id);
            }
        }

        //método para consultar o somatório da quantidade de produtos
        //para cada categoria do banco de dados
        public List<CategoryProductsResponseDto> GroupByCategoria()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Product>() //Tabela de produtos
                    .Include(p => p.Category) //Junção com a tabela de categoria
                    .GroupBy(p => p.Category.Name) //Agrupando pelo nome da categoria
                    .Select(g => new CategoryProductsResponseDto
                    {
                        Category = g.Key, //Nome da categoria
                        Products = g.Sum(p => p.Quantity) //Somatório da quantidade de produtos
                    })
                    .ToList(); //retonar uma lista do DTO
            }
        }
    }
}
