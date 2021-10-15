using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class PurchasedItem
    {
        public PurchasedItem()
        {
            Id = new Guid();
            //ActivationKey = Guid.NewGuid().ToString();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ActivationKey { get; set; }
        [Required]
        public virtual Guid ItemId { get; set; }
        [Required]
        public virtual Guid PurchaseId { get; set; }
    }
}
