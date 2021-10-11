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

        [Required]
        public string ActivationKey { get; set; }
        //public int stockleft { get; set; }
        private string CreateActivationKey()
        {

            var activationKey = Guid.NewGuid().ToString();

            List<Purchase> item = dbContext.Purchases.Where(x => x.ActivationKey == x.ActivationKey).ToList();
            IEnumerable<string> iter =
                from i in item
                select i.ActivationKey;

            List<string> keylist = iter.ToList();

            var exists = keylist.Any(key => key == activationKey);


            if (exists)
            {
                activationKey = CreateActivationKey();
            }

            return activationKey;

        }
    }
}
