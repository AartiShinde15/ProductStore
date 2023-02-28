using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
        
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        
        [Range(1,5)]
        public int Rating { get; set; }

    }
}
