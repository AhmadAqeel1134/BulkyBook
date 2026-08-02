using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{

    //name of class = name of table
    //properties of class = columns of table
    public class Category
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
