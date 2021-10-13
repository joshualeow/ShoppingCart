using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Purchase
    {
        public Purchase()
        {
            Id = new Guid();
        }
        //[Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public virtual ICollection<PurchasedItem> PurchasedItems { get; set; }
        [Required]
        public virtual User Customer { get; set; }
    }
}
