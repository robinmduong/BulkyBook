using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.Models
{
    public class Product //Step 1 for Product
    {

        [Key] //this key is technically optional since+
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        //Price if order <50 copies
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        //Price if you ordered between 50-99 copies
        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        //Price if order 100+ copies
        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        //Add foreign key references
        [ForeignKey("CategoryId")] //CategoryId is the name of the property
        public Category Category { get; set; } //the CategoryId is a foreign key of the Category table or model

        [Required]
        public int CoverTypeId { get; set; }

        [ForeignKey("CoverTypeId")]
        public CoverType CoverType { get; set; }
    }
}
