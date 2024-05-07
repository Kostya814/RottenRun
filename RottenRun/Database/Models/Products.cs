using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryShop.Database.Models;

public class Products
{
    public int Id { get; set; }
    public string Name{ get; set; }
    public string Specifications { get; set; }
    public int Price { get; set; }
    public string Image { get; set; }
    public int CategoryId{ get; set; }
    [NotMapped] public bool IsLike { get; set; } = false;
    
    public Categories Category{ get; set; }
    
    public List<Ratings> RatingsList { get; set; } = new List<Ratings>();
    public List<Baskets> ListBasket { get; set; } = new List<Baskets>();
    public List<FavoriteProducts> FavoriteProductsList { get; set; } = new List<FavoriteProducts>();
}