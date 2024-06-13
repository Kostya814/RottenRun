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
                TempData["TitleNotification"] = "Успешно";
                TempData["Notification"] = "Вход успешно осуществлен";
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
        TempData["TitleNotification"] = "Успешно";
        TempData["Notification"] = "Выход успешно осуществлен";
        return RedirectToAction("Index", "Home");        
    }
    [HttpPost]
    public IActionResult EditUser(int id ,string login , string password,string name , string email)
    {
        if(!ModelState.IsValid)
            return RedirectToAction("Index");
        var editUser = _context.Users.FirstOrDefault(u => u.Id == id);
        if(editUser == null)
            return RedirectToAction("Index");
        try
        {
            editUser.Login = login;
            editUser.Password = password;
            editUser.Name = name;
            editUser.Email = email;
            _context.SaveChanges();
            TempData["TitleNotification"] = "Успешно";
            TempData["Notification"] = "Данные текущего пользователя успешно изменены";
        }
        catch (Exception e)
        {
           
        }
        Response.Cookies.Delete("user");
        Response.Cookies.Append("user",JsonConvert.SerializeObject(editUser));
        return RedirectToAction("Index");
    }
}