using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RottenRun.Controllers;

public class BasketController : Controller
{
    private DBContext _context = new DBContext();
    private Users _user;

    public void LoadUser()
    {
        if(Request.Cookies.ContainsKey("user"))
            _user = JsonConvert.DeserializeObject<Users>(Request.Cookies["user"]);
    }

    public IActionResult Index()
    {
        LoadUser();
        if(_user == null) return RedirectToAction("Index","Profile");
        _context.Products.ToList();
        _context.Baskets.ToList();
        _context.Users.ToList();
        var listOrders = _context.Orders.Where(o=>o.User == _user).ToList();
        ViewBag.AllPrice = 0;
        foreach (var order in listOrders)
        {
            ViewBag.AllPrice += order.Basket.Product.Price*order.Basket.Count;
        }
        
        return View(listOrders);
    }

    [HttpPost]
    public IActionResult AddCount(int id)
    {
        var basket = _context.Baskets.FirstOrDefault(b=>b.Id==id);
        if(basket == null) return RedirectToAction("Index");
        basket.Count += 1;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult RemoveCount(int id)
    {
        var basket = _context.Baskets.FirstOrDefault(b=>b.Id==id);
        if(basket == null) return RedirectToAction("Index");
        if(basket.Count <= 0) return RedirectToAction("Index");
        basket.Count -= 1;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveBasket(int id)
    {
        var basket = _context.Baskets.FirstOrDefault(b=>b.Id==id);
        if (basket == null) return RedirectToAction("Index");
        var order = _context.Orders.FirstOrDefault(o => o.Basket.Id == basket.Id);
        if(order == null) return RedirectToAction("Index");
        _context.Orders.Remove(order);
        _context.Baskets.Remove(basket);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult CreateOrder()
    {
        
        return RedirectToAction("Index");
    }
}