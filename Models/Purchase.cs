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

        public Guid Id { get; set;}

        public DateTime PurchaseDate { get; set; }

        public int Quantity { get; set; }

        public ICollection<PurchasedItem> PurchasedItems { get; set; }
        //public int stockleft { get; set; }

    }
}
