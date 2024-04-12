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
    public IActionResult Reg(string login, string password,string email, string name )
    {
        
        var newUser = new Users()
        {
            Login = login,
            Email = email,
            Password = password,
            Name = name,
            Role = _context.Roles.FirstOrDefault()
        };
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}