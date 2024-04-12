using System.Collections.Generic;

namespace DeliveryShop.Database.Models;

public class Users
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
    public List<Ratings> RatingsList { get; set; } = new List<Ratings>();
    public List<Orders> OrdersList { get; set; } = new List<Orders>();
}