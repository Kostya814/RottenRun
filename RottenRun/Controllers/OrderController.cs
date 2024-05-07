using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RottenRun.Controllers;

public class OrderController : Controller
{
    private DBContext _context = new DBContext();
    private Users user;
    public IActionResult Index()
    {
        LoadUser();
        if (user == null)
            return RedirectToAction("Log", "Profile");
        _context.Baskets.ToList();
        _context.Statuses.ToList();
        _context.Addresses.ToList();
        var orderList = _context.Orders.Where(order =>  order.User.Id == user.Id).ToList();
        foreach (var order in orderList)
        {
            order.CountPrice();;
        }
        return View(orderList);
    }
    public void LoadUser()
    {
        if(Request.Cookies.ContainsKey("user"))
            user = JsonConvert.DeserializeObject<Users>(Request.Cookies["user"]);
    }
}