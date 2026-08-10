using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        [Required]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000, ErrorMessage = "List Price must be between 1 and 1000")]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        [Display(Name = "Price for 1-50")]
        public double PriceTillFifty { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = " Price must be between 1 and 1000")]
        [Display(Name = "Price for 50+")]
        public double PriceFiftyPlus { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = " Price must be between 1 and 1000")]
        [Display(Name = "Price for 100+")]
        public double PriceHundredPlus { get; set; }


        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Display(Name = "Product Image")]
        public string? ImageUrl { get; set; }
    }
}