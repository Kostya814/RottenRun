using System.Collections.Generic;

namespace DeliveryShop.Database.Models;

public class Manufacturers
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Products> ProductsList { get; set; } = new List<Products>();
}