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
            ActivationKey = Guid.NewGuid().ToString();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ActivationKey { get; set; }
        [Required]
        public virtual Guid ItemId { get; set; }
        [Required]
        public virtual Guid PurchaseId { get; set; }

        // private string CreateActivationKey()
        // {
        //     var activationKey = Guid.NewGuid().ToString();

        //     List<PurchasedItem> item = dbContext.PurchasedItems.Where(x => x.ActivationKey == x.ActivationKey).ToList();
        //     IEnumerable<string> iter =
        //         from i in item
        //         select i.ActivationKey;

        //     List<string> keylist = iter.ToList();

        //     var exists = keylist.Any(key => key == activationKey);

        //     if (exists) //If there is a same one
        //     {
        //         activationKey = CreateActivationKey();
        //     }

        //     return activationKey;
        // }
    }
}
