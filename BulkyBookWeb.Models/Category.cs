using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{

    //name of class = name of table
    //properties of class = columns of table
    public class Category
    {
        //[Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; } = string.Empty;


        [Required]
        [Display(Name = "Display Order")]
        [Range(0,100,ErrorMessage="Display Order must be between 0 and 100")]
        public int DisplayOrder { get; set; }




    }
}
