using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace RottenRun.Controllers;

public class RegistrationController : Controller
{
    private DBContext _context = new DBContext();
    public IActionResult Index()
    {
        return RedirectToAction("Reg");
    }
    [HttpGet]
    public IActionResult Reg()
    {
       return View();
    }

    [HttpPost]
    public IActionResult Reg(string login, string password,string email, string name, string repeatPassword)
    {
        if (password != repeatPassword) return RedirectToAction("Reg");
        
        var newUser = new Users()
        {   
            Login = login,
            Email = email,
            Password = password,
            Name = name,
            Role = _context.Roles.FirstOrDefault()
        };

        _context.Users.Add(newUser);
        var order = new Orders()
        {
            Status = _context.Statuses.FirstOrDefault(s=>s.Id == 4),
            User = newUser
        };
        _context.Orders.Add(order);      
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}