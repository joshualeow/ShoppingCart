using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Visitor
    {
        public Visitor()
        {
            Id = new Guid();
            Purchases = new List<Purchase>();
        }
        [Key]
        public Guid Id { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
