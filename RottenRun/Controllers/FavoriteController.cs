using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RottenRun.Controllers;

public class FavoriteController : Controller
{
    private DBContext _context = new DBContext();
    private Users user;
    public void LoadUser()
    {
        if(Request.Cookies.ContainsKey("user"))
            user = JsonConvert.DeserializeObject<Users>(Request.Cookies["user"]);
    }
    public IActionResult Index()
    {
        LoadUser();
        if (user == null)
            return RedirectToAction("Log","Profile");
        _context.Categories.ToList();
        _context.Products.ToList();
        _context.FavoriteProducts.ToList();
        _context.Users.ToList();
        var listProducts = _context.Products.ToList();
        foreach (var product in listProducts)
        {
            if(user==null)
                break;
            if (product.FavoriteProductsList.FirstOrDefault(u=>u.User.Id==user.Id) != null)
                product.IsLike = true;
        }

        var favoritelist = listProducts.Where(p => p.IsLike).ToList();
        return View(favoritelist);
    }
    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        LoadUser();
        if(user == null) 
            return RedirectToAction("Index","Profile");
        var existingProduct = _context.Products.Find(id);
        if(existingProduct == null)  
            return RedirectToAction("Index");
        _context.Baskets.ToList();
        _context.Products.ToList();
        Baskets basket = new Baskets();
        var orderUser = _context.Orders.OrderBy(o=>o.Id).LastOrDefault(o => o.User.Id == user.Id);
        if (orderUser == null) 
            orderUser = new Orders()
            {
                User = _context.Users.FirstOrDefault(u=>u.Id==user.Id), 
                Status = _context.Statuses.FirstOrDefault(s=>s.Id == 4)
            };
        foreach (var orderBasket in orderUser.BasketsList)
        {
            if(existingProduct.Id == orderBasket.Product.Id) 
                return RedirectToAction("Index");
        }
        basket.Product = existingProduct;
        basket.Count += 1;
        basket.Order = orderUser;
        _context.Baskets.Add(basket);
        TempData["TitleNotification"] = "Успешно";
        TempData["Notification"] = $"Товар {existingProduct.Name} добавлен в корзину";
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult RemoveToFavorite(int id)
    {
        LoadUser();
        _context.Products.ToList();
        _context.Users.ToList();
        if(user == null) 
            return RedirectToAction("Index","Profile");
        var existingProduct = _context.Products.Find(id);
        if(existingProduct == null)  
            return RedirectToAction("Index");
        var favoriteProduct = _context.FavoriteProducts.FirstOrDefault(f => f.User.Id == user.Id && f.Product.Id == existingProduct.Id);
        if (favoriteProduct == null) return RedirectToAction("Index");
        _context.Remove(favoriteProduct);
        _context.SaveChanges();
        TempData["TitleNotification"] = "Успешно";
        TempData["Notification"] = $"Товар {existingProduct.Name} убран из избранное";
        return RedirectToAction("Index");
    }
}