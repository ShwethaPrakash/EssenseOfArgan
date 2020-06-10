using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Repository;
using Repository.Models;

namespace EssenseOfArgan.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository orderRepository;
        
        public OrderController(IOrderRepository repository)
        {
            orderRepository = repository;
        }
        // GET: Order
        public ActionResult Index()
        {
            var orders = orderRepository.GetOrders();
            return View(orders);
        }

        [HttpPost]
        public ActionResult Index(IList<Repository.Models.Order> orderToShip)
        {
            var shippingOrders = orderToShip.Where(x => x.IsSelected == true) ;
            var jsonResult = CreateJSonRequest(shippingOrders.ToList());
            TempData["jsonresult"] = jsonResult;
            return RedirectToAction("ShipmentDetails","Order");
        }

        public ActionResult ShipmentDetails(string result)
        {
            //var jsonresult = TempData["jsonresult"].ToString();
            return View();
        }
        private string CreateJSonRequest(IList<Repository.Models.Order> orderToShip)
        {
            int shipmentId = 1;
            var groupByShippingLogic = orderToShip.GroupBy(x => new
            {
                x.Category,
                x.Address,
                x.City,
                x.State,
                x.Country
            }).Select(y => new
            {
                Category = y.Key.Category,
                Address = y.Key.Address,
                City = y.Key.City,
                State = y.Key.State,
                Country = y.Key.Country,
                Products = y.ToList()
            });

            var shipments = new List<Repository.Models.Shipment>();

            foreach(var item in groupByShippingLogic)
            {
                var shipmentDetails = new Shipment
                {
                    ShipmentId = $"ShipmentID-{shipmentId++}",
                    Address = item.Address,
                    City = item.City,
                    State = item.State,
                    Country = item.Country,
                    Products = new List<Product>()
                    
                };

                foreach(var product in item.Products)
                {
                    shipmentDetails.FirstName = product.FirstName;
                    shipmentDetails.LastName = product.LastName;
                    shipmentDetails.Products.Add(new Product
                    {
                        ProductName = product.ProductName,
                        Description = product.Description,
                        SKU = product.SKU,
                        Price = product.Price,
                        Quantity = product.Quantity
                    });
                }

                shipments.Add(shipmentDetails);
            }

            var shipmentsInJson = JsonConvert.SerializeObject(shipments,Formatting.Indented);
            return shipmentsInJson;
                                          
        }
    }
}