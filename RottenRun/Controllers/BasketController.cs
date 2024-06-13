using System.Text.Json;
using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RottenRun.Controllers;

public class BasketController : Controller
{
    private DBContext _context = new DBContext();
    private Users _user;
    private Orders _currentOrder;

    public void LoadUser()
    {
        if(Request.Cookies.ContainsKey("user"))
            _user = JsonConvert.DeserializeObject<Users>(Request.Cookies["user"]);
    }
    
    public IActionResult Index()
    {
        
        LoadUser();
        if(_user == null) 
            return RedirectToAction("Index","Profile");
        _context.Products.ToList();
        _context.Baskets.ToList();
        _context.Users.ToList();
        _context.Addresses.ToList();
        _currentOrder = _context.Orders.OrderBy(o => o.Id)
            .LastOrDefault(O => O.User.Id == _user.Id);
        if (_currentOrder == null)
            return RedirectToAction("Index","Home");
        ViewBag.currentOrder = _currentOrder;
        var listBaskets = _context.Baskets.Where(b =>
            b.Order.Id == _currentOrder.Id).OrderBy(o=>o.Id).ToList();
        ViewBag.AllPrice = 0;
        foreach (var basket in listBaskets)
        {
            ViewBag.AllPrice += basket.Product.Price*basket.Count;
        }
        return View(listBaskets);
    }

    [HttpPost]
    public IActionResult AddCount(int id)
    {
        var basket = _context.Baskets.FirstOrDefault(b=>b.Id==id);
        if(basket == null) 
            return RedirectToAction("Index");
        basket.Count += 1;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult RemoveCount(int id)
    {
        var basket = _context.Baskets.FirstOrDefault(b=>b.Id==id);
        if(basket == null) 
            return RedirectToAction("Index");
        if(basket.Count <= 1) 
            return RedirectToAction("Index");
        basket.Count -= 1;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveBasket(int id)
    {
        var basket = _context.Baskets.FirstOrDefault(b=>b.Id==id);
        if (basket == null) 
            return RedirectToAction("Index");
        _context.Baskets.Remove(basket);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult CreateOrder()
    {
        LoadUser();
        if(_user == null) 
            return RedirectToAction("Index","Profile");
        var order = _context.Orders.OrderBy(o => o.Id)
            .LastOrDefault(O => O.User.Id == _user.Id);
        _context.Baskets.ToList();
        if(order.BasketsList.Count == 0) 
            return RedirectToAction("Index");
        order.Status = _context.Statuses.FirstOrDefault(s => s.Id == 3);
        order.CreationDate = DateTime.UtcNow;
        var newOrder = new Orders()
        {
            User = _context.Users.FirstOrDefault(u=>u.Id == _user.Id),
            Status = _context.Statuses.FirstOrDefault(s => s.Id == 4)
        };
        _context.Orders.Add(newOrder);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AddDeliveryAddress([FromBody]JsonElement json)
    {
        if(!ModelState.IsValid)
            return RedirectToAction("Index");
        var address = json.Deserialize<string>();
        var addressPaths = address.Split(",");
        string city = addressPaths[0];
        string street = addressPaths[1];
        string home = addressPaths[2];
        LoadUser();
        if(_user == null) 
            return RedirectToAction("Index","Profile");
        var order = _context.Orders.OrderBy(o => o.Id)
            .LastOrDefault(O => O.User.Id == _user.Id);
        order.DeliveryAddresses = new Addresses()
        {
            City = city,
            Street = street,
            Home = home,
            ApartmentNumber = 1
        };
        _context.SaveChanges();
        TempData["TitleNotification"] = "Успешно";
        TempData["Notification"] = $"Адрес успешно добавлен";
        return RedirectToAction("Index","Basket");
    }
}