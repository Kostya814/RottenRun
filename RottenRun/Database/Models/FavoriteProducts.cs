namespace DeliveryShop.Database.Models;

public class FavoriteProducts
{
    public int Id { get; set; }
    public Products Product { get; set; }
    public Users User { get; set; }
}