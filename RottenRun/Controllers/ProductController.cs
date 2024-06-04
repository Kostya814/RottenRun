using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RottenRun.Controllers;

public class ProductController : Controller
{
    private DBContext db = new DBContext(); 
    private Users user;
    public IActionResult Index(int id)
    {
        LoadUser();
        db.FavoriteProducts.ToList();
        db.Categories.ToList();
        db.Users.ToList();
        if (!ModelState.IsValid)
            return RedirectToAction("Index", "Home");
        var product = db.Products.FirstOrDefault(p => p.Id == id);
        if (product.FavoriteProductsList.FirstOrDefault(u=>u.User.Id==user.Id) != null)
            product.IsLike = true;
        if(product == null)
            return RedirectToAction("Index", "Home");
        return View(product);
    }
    public void LoadUser()
    {
        if(Request.Cookies.ContainsKey("user"))
            user = JsonConvert.DeserializeObject<Users>(Request.Cookies["user"]);
    }
    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        LoadUser();
        if(user == null) 
            return RedirectToAction("Index","Profile");
        var existingProduct = db.Products.Find(id);
        if(existingProduct == null)  
            return RedirectToAction("Index");
        db.Baskets.ToList();
        db.Products.ToList();
        Baskets basket = new Baskets();
        var orderUser = db.Orders.OrderBy(o=>o.Id).LastOrDefault(o => o.User.Id == user.Id);
        if (orderUser == null) 
            orderUser = new Orders()
            {
                User = db.Users.FirstOrDefault(u=>u.Id==user.Id), 
                Status = db.Statuses.FirstOrDefault(s=>s.Id == 4)
            };
        foreach (var orderBasket in orderUser.BasketsList)
        {
            if(existingProduct.Id == orderBasket.Product.Id) 
                return RedirectToAction("Index");
        }
        basket.Product = existingProduct;
        basket.Count += 1;
        basket.Order = orderUser;
        db.Baskets.Add(basket);
        db.SaveChanges();
        return RedirectToAction("Index", new { id = id });
    }
    [HttpPost]
    public IActionResult AddToFavorite(int id)
    {
        LoadUser();
        db.Products.ToList();
        db.Users.ToList();
        if(user == null) 
            return RedirectToAction("Index","Profile");
        var existingProduct = db.Products.Find(id);
        if(existingProduct == null)  
            return RedirectToAction("Index");
        var favoriteProduct = db.FavoriteProducts.FirstOrDefault(f => f.User.Id == user.Id && f.Product.Id == existingProduct.Id);
        if (favoriteProduct != null) 
            return RedirectToAction("Index");
        db.FavoriteProducts.Add(new FavoriteProducts()
        {
            User = db.Users.FirstOrDefault(u=>u.Id == user.Id),
            Product = existingProduct
        });
        db.SaveChanges();
        return RedirectToAction("Index", new { id = id });
    }
    public IActionResult RemoveToFavorite(int id)
    {
        LoadUser();
        db.Products.ToList();
        db.Users.ToList();
        if(user == null) 
            return RedirectToAction("Index","Profile");
        var existingProduct = db.Products.Find(id);
        if(existingProduct == null)  
            return RedirectToAction("Index");
        var favoriteProduct = db.FavoriteProducts.FirstOrDefault(f => f.User.Id == user.Id && f.Product.Id == existingProduct.Id);
        if (favoriteProduct == null) return RedirectToAction("Index");
        db.Remove(favoriteProduct);
        db.SaveChanges();
        return RedirectToAction("Index", new { id = id });
    }
}