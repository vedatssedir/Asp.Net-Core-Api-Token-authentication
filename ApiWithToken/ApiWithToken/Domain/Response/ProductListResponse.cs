using System.Collections;
using System.Collections.Generic;
using ApiWithToken.Domain.Entities;

namespace ApiWithToken.Domain.Response
{
    public class ProductListResponse:BaseResponse
    {
        public IEnumerable<Product> Products { get; set; }
        private ProductListResponse(bool success, string message,IEnumerable<Product> products) : base(success, message)
        {
            Products = products;
        }


        public ProductListResponse(IEnumerable<Product> products):this(true,string.Empty,products)
        {
            
        }

        public ProductListResponse(string message):this(false,message,default)
        {
            
        }
        
        
    }
}