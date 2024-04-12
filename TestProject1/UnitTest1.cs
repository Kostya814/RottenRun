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
    public void Index_RedirectsToLogOut_WhenUserCookieExists()
    {
        // Arrange
        var mockHttpContext = new Mock<HttpContext>();
        var mockHttpRequest = new Mock<HttpRequest>();
        var mockHttpResponse = new Mock<HttpResponse>();

        
        var user = new Users { };
        var userJson = JsonConvert.SerializeObject(user);

        mockHttpContext.Setup(c => c.Request).Returns(mockHttpRequest.Object);
        mockHttpContext.Setup(c => c.Response).Returns(mockHttpResponse.Object);
        mockHttpRequest.Setup(r => r.Cookies.ContainsKey("user")).Returns(true);
        mockHttpRequest.Setup(r => r.Cookies["user"]).Returns(userJson);

        var controller = new ProfileController()
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            }
        };

        // Act
        var result = controller.Index();

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.Equal("LogOut", redirectToActionResult.ActionName);
    }
}