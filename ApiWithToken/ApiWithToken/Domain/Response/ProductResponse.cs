using ApiWithToken.Domain.Entities;

namespace ApiWithToken.Domain.Response
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; }

        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            this.Product = product;
        }
        //Basarılı olursa

        public ProductResponse(Product product):this(true,string.Empty,product)
        {
            
        }
        //
        //Basarısız olursa
        public ProductResponse(string message):this(false,message,default)
        {
            
        }
        
        
        
        
    }
}