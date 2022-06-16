using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductAPI.Data.ValueObjects
{
    public class ProductVO
    {
        public int Id { get; set; }
       
        public string? Name { get; set; }

        public decimal Price { get; set; }
 
        public string? Description { get; set; }
  
        public decimal CategoryName { get; set; }
 
        public string ImageUrl { get; set; }
    }
}
