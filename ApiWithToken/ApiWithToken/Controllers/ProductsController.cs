using ApiWithToken.Domain.Entities;
using ApiWithToken.Domain.Services;
using ApiWithToken.Extensions;
using ApiWithToken.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ApiWithToken.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var productListResponse = await _productService.ListAsync();
            if (productListResponse.Success)
            {
                return Ok(productListResponse.Products);
            }

            return BadRequest(productListResponse.Message);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            var productResponse = await _productService.FindByIdAsync(id);
            if (productResponse.Success)
            {
                return Ok(productResponse);
            }

            return BadRequest(productResponse.Message);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var product = _mapper.Map<ProductResource, Product>(productResource);
            var productResponse = await _productService.AddProduct(product);
            if (productResponse.Success)
            {
                return Ok(productResponse.Product);
            }
            return BadRequest(productResponse.Message);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductResource productResource, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var product = _mapper.Map<ProductResource, Product>(productResource);
            var response = await _productService.UpdateProduct(product, productId);
            if (response.Success)
            {
                return Ok(response.Product);
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var response = await _productService.RemoveProduct(id);
            if (response.Success)
            {
                return Ok(response.Product);
            }
            return BadRequest(response.Message);
        }
    }
}