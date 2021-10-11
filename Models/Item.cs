using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Item
    {
        public Item()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public float Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }

        

        [MaxLength(256)]
        public string Description { get; set; } 

        public virtual User User { get; set; }

        //
    }
}
