using System.Collections.Generic;

namespace DeliveryShop.Database.Models;

public class Products
{
    public int ID { get; set; }
    public string Name{ get; set; }
    public string Specifications { get; set; }
    public int Price { get; set; }
    public string Image { get; set; }
    public int CategoryId{ get; set; }
    public Categories Category{ get; set; }
    
    public List<Ratings> RatingsList { get; set; } = new List<Ratings>();
    public List<Basket> ListBasket { get; set; } = new List<Basket>();
}