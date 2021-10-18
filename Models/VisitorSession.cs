using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class VisitorSession
    {
        public VisitorSession()
        {
            Id = new Guid();
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public Guid Id { get; set; }

        public long Timestamp { get; set; }

        public virtual User User { get; set; }
    }
}
