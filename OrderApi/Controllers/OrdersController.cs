using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Models;
using RestSharp;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OrdersController : ControllerBase
    {
        private readonly IRepository<BEOrder> repository;

        public OrdersController(IRepository<BEOrder> repos)
        {
            repository = repos;
        }

        // GET: orders
        [HttpGet]
        public IEnumerable<BEOrder> Get()
        {
            return repository.GetAll();
        }

        // GET orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST orders
       [HttpPost]
        public IActionResult Post([FromBody]BEOrder order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            
            RestClient c = new RestClient();
            RestClient c2 = new RestClient();
          
            var productCheck = false;
            var customerCheck = false;
            var totalprice = 0.0;

            // Call ProductApi to get the product ordered and the customer
            c.BaseUrl = new Uri("https://localhost:5001/products/"+order.ProductId);
            c2.BaseUrl = new Uri("https://localhost:44399/customers/"+order.CustomerId);

            var request = new RestRequest(Method.GET);
            var request2 = new RestRequest(Method.GET);

            var response = c.Execute<BEProduct>(request);
            var response2 = c2.Execute<BECustomer>(request2);

            var orderedProduct = response.Data;
            var customerOrdering = response2.Data;

            if (order.Quantity <= orderedProduct.ItemsInStock - orderedProduct.ItemsReserved)
                productCheck = true;

            if (customerOrdering.CreditStanding >= totalprice)
                customerCheck = true;

            if (customerCheck == productCheck == true)
            {
                orderedProduct.ItemsReserved += order.Quantity;
                var updateRequestProduct = new RestRequest("https://localhost:5001/products/" + orderedProduct.Id.ToString(), Method.PUT);
                updateRequestProduct.AddJsonBody(orderedProduct);
                var updateResponseProduct = c.Execute(updateRequestProduct);
                totalprice = (double)(orderedProduct.Price * order.Quantity);

                customerOrdering.CreditStanding -= totalprice;
                var updateRequestCustomer = new RestRequest("https://localhost:44399/customers/" + customerOrdering.Id.ToString(), Method.PUT);
                updateRequestCustomer.AddJsonBody(customerOrdering);
                var updateResponseCustomer = c2.Execute(updateRequestCustomer);

                if(updateResponseProduct.IsSuccessful && updateResponseCustomer.IsSuccessful)
                {
                    var newOrder = repository.Add(order);
                    return CreatedAtRoute("GetOrder", new { id = newOrder.Id }, newOrder);
                }

            }

                // If the order could not be created, "return no content".
                return NoContent();
        }

    }
}
