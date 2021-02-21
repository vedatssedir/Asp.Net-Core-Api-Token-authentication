using System.Collections.Generic;
using System.Threading.Tasks;
using ApiWithToken.Domain.Entities;

namespace ApiWithToken.Domain.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(int productId);
        void UpdateProduct(Product product);
        Task<Product> FindByIdAsync(int productId);
    }
}