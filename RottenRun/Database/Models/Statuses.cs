using System.Collections.Generic;

namespace DeliveryShop.Database.Models;

public class Statuses
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Orders> OrdersList { get; set; } = new List<Orders>();
}