using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data.Entity.Core.Objects;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        public EssenseOfArganEntities entities;
        public OrderRepository()
        {
            entities = new EssenseOfArganEntities();
        }

        public List<Order> GetOrders()
        {
            var result = entities.GetOrders();
            var orders = MapOrders(result);

            return orders;
        }

        public List<Order> MapOrders(ObjectResult<GetOrders_Result> order_result)
        {
            var orders = new List<Order>();

            foreach(var result in order_result)
            {
                orders.Add(new Order
                {
                    IsSelected = false,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    City = result.City,
                    State = result.State,
                    Address = result.Address,
                    Country = result.Country,
                    ProductName = result.ProductName,
                    Category = result.Category,
                    Price = result.Price,
                    SKU = result.ProductSKU,
                    Quantity = result.quantity
                }) ;
            }

            return orders;
        }
        
    }
}
