using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RottenRun.Models;

namespace RottenRun.Controllers;

public class HomeController : Controller
{
    private Users user;
    private readonly ILogger<HomeController> _logger;
    private DBContext context = new DBContext();
    private ClaimsIdentity _claimsIdentity; 

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public void LoadUser()
    {
        if(Request.Cookies.ContainsKey("user"))
            user = JsonConvert.DeserializeObject<Users>(Request.Cookies["user"]);
    }

    public IActionResult Index()
    {
        LoadUser();
        context.Categories.ToList();
        var ListProducts = context.Products.ToList();
        return View(context.Products.ToList());
    }
    [HttpGet]
    public IActionResult Index(string textSearch)
    {
        var ListProducts = context.Products.ToList();
        if (ModelState.IsValid)
        {
            context.Categories.ToList();
            var findSerch = ListProducts.FindAll(u => u.Name.Contains(textSearch)).ToList();
            return View(findSerch);
        }
        return View(ListProducts);
        
    }

    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        LoadUser();
        if(user == null) return RedirectToAction("Index");
        var existingProduct = context.Products.Find(id);
        if(existingProduct == null)  return RedirectToAction("Index");
        Basket basket = new Basket();
        basket.Product = existingProduct;
        basket.Count += 1;
        context.Products.ToList();
        context.Baskets.ToList();
        context.Users.ToList();
        List<Orders> listOrders = context.Orders.Where(o => o.User == user).ToList();
        foreach (var orders in listOrders)
        {
            if (orders.Basket.Product.ID != existingProduct.ID)
                continue;
            orders.Basket.Count += 1;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        context.Baskets.Add(basket);
        var order = new Orders()
        {
            UsersId = user.Id,
            Status = context.Statuses.FirstOrDefault(),
            Basket = basket
        };
        context.Orders.Add(order);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}