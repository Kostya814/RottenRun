using DeliveryShop.Database;
using DeliveryShop.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RottenRun.Controllers;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        DBContext context = new DBContext();
        Users user = new Users()
        {
            Name = "Kostya",
            Email = "тыва@gmail.com",
            Password = "password",
            Login = "login",
            Id = 1,
            Role = context.Roles.FirstOrDefault()
        };
        
        // Arrange
        var findUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
        // Assert
        Assert.Equal(user.Id, findUser.Id);
        Assert.Equal(user.Name, findUser.Name);
        Assert.Equal(user.Password, findUser.Password);

    }
    [Fact]
    public void LogOut_Test()
    {
        // Arrange
        var controller = new ProfileController();
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Cookies.Append("user", "someUser");
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };

        // Act
        var result = controller.LogOut("a") as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Home", result.ControllerName);
        Assert.Equal("Index", result.ActionName);
        Assert.False(httpContext.Request.Cookies.ContainsKey("user"));
    }
}

    
