using System.Collections.Generic;
using System.Threading.Tasks;
using ApiWithToken.Domain.Contexts;
using ApiWithToken.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiWithToken.Domain.Repository
{
    public class ProductRepository :BaseRepository, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Product>> ListAsync()
        {
            var data = await Context.Products.ToListAsync();
            return data;
        }
        public async Task AddProductAsync(Product product)
        {
            await Context.Products.AddAsync(product);
        }

        public async Task RemoveProductAsync(int productId)
        {
            var data = await Context.Products.FindAsync(productId);
            Context.Products.Remove(data);
        }

        public  void UpdateProduct(Product product)
        {
            Context.Products.Update(product);
        }
        public async Task<Product> FindByIdAsync(int productId)
        {
            return await Context.Products.FindAsync(productId);
        }
    }
}