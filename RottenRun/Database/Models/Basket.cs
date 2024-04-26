using System.Collections.Generic;

namespace DeliveryShop.Database.Models;

public class Baskets
{
    public int Id { get; set; }
    public Products Product { get; set; }
    public Orders Order { get; set; }
    public int Count { get; set; } = 0;
}