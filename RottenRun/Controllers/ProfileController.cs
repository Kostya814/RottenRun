using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RottenRun.Controllers;

public class ProfileController : Controller
{
    private DBContext _context= new DBContext();
    private Users user;
    public IActionResult Index()
    {
        LoadUser();
        if (user == null)
            return RedirectToAction("Log");
        return View(user);

    }
    public void LoadUser()
    {
        if(Request.Cookies.ContainsKey("user"))
            user = JsonConvert.DeserializeObject<Users>(Request.Cookies["user"]);
    }
    [HttpGet]
    public IActionResult Log()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Log(string login , string password)
    {
        if (ModelState.IsValid)
        {
            Users? user = _context.Users.FirstOrDefault(p => p.Login == login && p.Password == password);
            if(user == null) return RedirectToAction("Index");
                Response.Cookies.Append("user",JsonConvert.SerializeObject(user));
            
        }
        return RedirectToAction("Index","Home");
    }
    [HttpGet]
    public IActionResult LogOut()
    {
        return View();
    }
    [HttpPost]
    public IActionResult LogOut(string name)
    {
        Response.Cookies.Delete("user");
        return RedirectToAction("Index", "Home");
    }
}