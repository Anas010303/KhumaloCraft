using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using WebApplicationPOE1.Models;
using WebApplicationPOE1.ViewModels;

namespace WebApplicationPOE1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDurableClient _durableClient;

        public HomeController(ApplicationDbContext dbContext, IDurableClient durableClient)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _durableClient = durableClient ?? throw new ArgumentNullException(nameof(durableClient));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MyWork()
        {
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT ProductId, Name, Description, Price, ImageUrl FROM Products";
                _dbContext.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    var products = new List<ProductViewModel>();
                    while (result.Read())
                    {
                        products.Add(new ProductViewModel
                        {
                            ProductId = result.GetInt32(0),
                            Name = result.GetString(1),
                            Description = result.GetString(2),
                            Price = result.GetDecimal(3),
                            ImageUrl = result.GetString(4)
                        });
                    }
                    return View(products);
                }
            }
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddProduct", product);
        }

        public IActionResult ProductDetail(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(int productId, int quantity)
        {
            var product = _dbContext.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            var paymentViewModel = new PaymentViewModel
            {
                ProductId = productId,
                Quantity = quantity
            };

            return View("Payment", paymentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPayment(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Payment", model);
            }

            var product = _dbContext.Products.Find(model.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalAmount = product.Price * model.Quantity,
                UserId = GetUserId(),
                OrderDate = DateTime.Now
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            string instanceId = await _durableClient.StartNewAsync("OrderProcessingOrchestrator", order);
            return RedirectToAction("MyWork");
        }

        public IActionResult Orders()
        {
            var transactions = _dbContext.Transactions.ToList();
            return View(transactions);
        }

        public IActionResult ClientOrders()
        {
            var orders = _dbContext.Transactions.ToList();
            return View(orders);
        }

        private string GetUserId()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrEmpty(userId) ? "default_user_id" : userId;
        }
    }
}
