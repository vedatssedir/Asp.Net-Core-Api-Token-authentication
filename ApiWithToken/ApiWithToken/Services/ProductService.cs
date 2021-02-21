using System;
using System.Threading.Tasks;
using ApiWithToken.Domain.Entities;
using ApiWithToken.Domain.Repository;
using ApiWithToken.Domain.Response;
using ApiWithToken.Domain.Services;
using ApiWithToken.Domain.UnitOfWork;

namespace ApiWithToken.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ProductListResponse> ListAsync()
        {
            try
            {
                var productList = await _productRepository.ListAsync();
                return new ProductListResponse(productList);
            }
            catch (Exception ex)
            {
                return new ProductListResponse($"Ürün listelenirken bir hata meydana geldi :{ex.Message}");
            }
        }
        public async Task<ProductResponse> AddProduct(Product product)
        {
            try
            {
                await _productRepository.AddProductAsync(product);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün eklenirken bir hata meydana geldi :{ex.Message}");
            }
        }
        public async Task<ProductResponse> RemoveProduct(int productId)
        {
            try
            {
                var product = await _productRepository.FindByIdAsync(productId);
                if (product == null)
                {
                    return new ProductResponse("Silme calıştıgınız ürün  bulunamamıştır");
                }

                await _productRepository.RemoveProductAsync(productId);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün bulunurken bir hata meydana geldi :{ex.Message}");
            }
        }
        public async Task<ProductResponse> UpdateProduct(Product product, int productId)
        {
            try
            {
                var products = await _productRepository.FindByIdAsync(productId);
                if (products==null)
                {
                    return new ProductResponse("Güncellemeye calıştıgınız ürün  bulunamamıştır");
                }

                products.Name = product.Name;
                products.Category = product.Category;
                products.Price = product.Price;
                _productRepository.UpdateProduct(products);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün bulunurken bir hata meydana geldi :{ex.Message}");
            }
        }
        public async Task<ProductResponse> FindByIdAsync(int productId)
        {
            try
            {
                var product =await _productRepository.FindByIdAsync(productId);
                return product == null ? new ProductResponse("Ürün bulunamadı") : new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün bulunurken bir hata meydana geldi :{ex.Message}");
            }
        }
    }
}