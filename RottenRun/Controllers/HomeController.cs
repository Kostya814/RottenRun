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
        if(user == null) 
            return RedirectToAction("Index");
        var existingProduct = context.Products.Find(id);
        if(existingProduct == null)  
            return RedirectToAction("Index");
        context.Baskets.ToList();
        context.Products.ToList();
        Baskets basket = new Baskets();
        var orderUser = context.Orders.OrderBy(o=>o.Id).LastOrDefault(o => o.User.Id == user.Id);
        if (orderUser == null) 
            orderUser = new Orders()
            {
                User = context.Users.FirstOrDefault(u=>u.Id==user.Id), 
                Status = context.Statuses.FirstOrDefault(s=>s.Id == 4)
            };
        foreach (var orderBasket in orderUser.BasketsList)
        {
            if(existingProduct.Id == orderBasket.Product.Id) 
                return RedirectToAction("Index");
        }
        basket.Product = existingProduct;
        basket.Count += 1;
        basket.Order = orderUser;
        context.Baskets.Add(basket);
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