using ProductsAPI.Contexts;
using ProductsAPI.Entities;

namespace ProductsAPI.Repositories
{
    public class CategoryRepository
    {
        //método para consultar todas as categorias
        public List<Category> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Category>()
                    .OrderBy(c => c.Name)
                    .ToList();
            }
        }
    }
}
