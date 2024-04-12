using System.Collections.Generic;

namespace DeliveryShop.Database.Models;

public class Roles
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Users> UsersList { get; set; } = new List<Users>();
}