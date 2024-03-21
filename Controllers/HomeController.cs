using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationPOE1.Models;

namespace WebApplicationPOE1.Controllers
{
    public class HomeController : Controller
    {
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
            // Here you can fetch data from your database and pass it to the view
            // For simplicity, I'll just return the view without any data
            return View();
        }
    }
}
