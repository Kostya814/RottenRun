using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
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
    [HttpGet]
    public IActionResult Index()
    {
        LoadUser();
        var listCategories = context.Categories.ToList();
        context.Products.ToList();
        context.FavoriteProducts.ToList();
        context.Users.ToList();
        var listProducts = context.Products.ToList();
        foreach (var product in listProducts)
        {
            if(user==null)
                break;
            if (product.FavoriteProductsList.FirstOrDefault(u=>u.User.Id==user.Id) != null)
                product.IsLike = true;
        }
        ViewBag.Categorits = listCategories;
        return View(listProducts);
    }

    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        LoadUser();
        if(user == null) 
            return RedirectToAction("Index","Profile");
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
            if (existingProduct.Id != orderBasket.Product.Id)
                continue;
            TempData["TitleNotification"] = "Успешно";
            TempData["Notification"] = $"Товар {existingProduct.Name} уже в корзине";
            return RedirectToAction("Index");
        }
        basket.Product = existingProduct;
        basket.Count += 1;
        basket.Order = orderUser;
        context.Baskets.Add(basket);
        context.SaveChanges();
        TempData["TitleNotification"] = "Успешно";
        TempData["Notification"] = $"Товар {existingProduct.Name} добавлен в корзину";
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult AddToFavorite(int id)
    {
        LoadUser();
        context.Products.ToList();
        context.Users.ToList();
        if(user == null) 
            return RedirectToAction("Index","Profile");
        var existingProduct = context.Products.Find(id);
        if(existingProduct == null)  
            return RedirectToAction("Index");
        var favoriteProduct = context.FavoriteProducts.FirstOrDefault(f => f.User.Id == user.Id && f.Product.Id == existingProduct.Id);
        if (favoriteProduct != null) 
            return RedirectToAction("Index");
        context.FavoriteProducts.Add(new FavoriteProducts()
        {
            User = context.Users.FirstOrDefault(u=>u.Id == user.Id),
            Product = existingProduct
        });
        context.SaveChanges();
        TempData["TitleNotification"] = "Успешно";
        TempData["Notification"] = $"Товар {existingProduct.Name} добавлен в избранное";
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult RemoveToFavorite(int id)
    {
        LoadUser();
        context.Products.ToList();
        context.Users.ToList();
        if(user == null) 
            return RedirectToAction("Index","Profile");
        var existingProduct = context.Products.Find(id);
        if(existingProduct == null)  
            return RedirectToAction("Index");
        var favoriteProduct = context.FavoriteProducts.FirstOrDefault(f => f.User.Id == user.Id && f.Product.Id == existingProduct.Id);
        if (favoriteProduct == null) return RedirectToAction("Index");
        context.Remove(favoriteProduct);
        context.SaveChanges();
        TempData["TitleNotification"] = "Успешно";
        TempData["Notification"] = $"Товар {existingProduct.Name} убран из избранное";
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