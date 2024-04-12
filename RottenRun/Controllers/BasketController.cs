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
        if(_user == null) return RedirectToAction("Index","Home");
        _context.Products.ToList();
        _context.Baskets.ToList();
        _context.Users.ToList();
        var listOrders = _context.Orders.Where(o=>o.User == _user).ToList();
        return View(listOrders);
    }
}