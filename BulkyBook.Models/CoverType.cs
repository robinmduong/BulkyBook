using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.Models //(Step 1) Make a new model in BulkyBook.Models for CoverType
{
    public class CoverType
    {
        [Key] //import System.ComponentMOdel.DataAnotations
        public int Id { get; set; }

        [Display(Name = "Cover Type")] //make sure this Name is capitalized
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //(Step 2) Go to BulkyBook.DataAccess > Data > ApplicationDbContext.cs
    }
}
