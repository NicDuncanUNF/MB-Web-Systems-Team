using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

/*
 * To update the database after modifying this:
 * use NuGet pkg manager > console > Update-Database
 */
namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a product name")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please specify a sub-category")]
        public string Subcategory { get; set; }

        //TODO add a default error image
        //TODO find a way to enforce a .png or other valid image file extension
        [Required(ErrorMessage = "Please specify a web image address")]
        public string Image { get; set; }

    }
}
