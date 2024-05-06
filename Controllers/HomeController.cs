using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using WebApplicationPOE1.Models;
using WebApplicationPOE1.Models;
using WebApplicationPOE1.ViewModels;

namespace WebApplicationPOE1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IActionResult Index()
        {
            return View();
            //return Content("Hello, this is the Index method!");

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

    


      /* public IActionResult MyWork()
        {
            var products = _dbContext.Products.Select(p => new ProductViewModel
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
            }).ToList();

            return View(products);
        }*/
        // Handle form submission here



        
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


        // GET: Display the form to add a product
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: Handle form submission to add a product
        [HttpPost]
        [ValidateAntiForgeryToken] // Add anti-forgery token to prevent CSRF attacks
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

        // GET: Display the product detail page
        public IActionResult ProductDetail(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Handle form submission to place an order
        [HttpPost]
        [ValidateAntiForgeryToken] // Add anti-forgery token to prevent CSRF attacks
        public IActionResult PlaceOrder(int productId, int quantity)
        {
            var product = _dbContext.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }
            var transaction = new Transaction
            {
                ProductId = productId,
                Quantity = quantity,
                TotalAmount = product.Price * quantity, // Make sure Price is a numeric type
                TransactionDate = DateTime.Now
                // You may also set other transaction properties like UserId
            };
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return RedirectToAction("MyWork");
        }

        // GET: Display previous orders
        public IActionResult Orders()
        {
            // Retrieve previous orders from the database
            var transactions = _dbContext.Transactions.ToList();
            return View(transactions);
        }

        // Action method to display client orders
        public IActionResult ClientOrders()
        {
            // Retrieve client orders from the database
            var orders = _dbContext.Transactions.ToList();
            // Pass the list of orders to the view
            return View(orders);
        }

        // Method to get the current user's ID
        private string GetUserId()
        {
            // Retrieve the user's ID from the HttpContext
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // If user ID is not available (e.g., user not authenticated), return a default value or handle accordingly
            if (string.IsNullOrEmpty(userId))
            {
                return "default_user_id"; // Provide a default value or handle the scenario
            }

            return userId;
        }
    }
}
