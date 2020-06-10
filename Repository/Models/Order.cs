using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Order
    {
        public bool IsSelected { get; set; }
        public string OrderId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ProductName { get; set; }

        public string SKU { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public Nullable<decimal> Price { get; set; }

        public string Quantity { get; set; }
    }
}
