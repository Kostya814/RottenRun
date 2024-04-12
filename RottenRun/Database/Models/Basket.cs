using System.Collections.Generic;

namespace DeliveryShop.Database.Models;

public class Basket
{
    public int Id { get; set; }
    public Products Product { get; set; }
    public int Count { get; set; }
    public List<Orders> OrdersList { get; set; } = new List<Orders>();
}