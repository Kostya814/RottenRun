namespace DeliveryShop.Database.Models;

public class Addresses
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Home { get; set; }
    public int ApartmentNumber { get; set; }
    public List<Basket> ListBasket { get; set; } = new List<Basket>();
    public List<Orders> OrdersList { get; set; } = new List<Orders>();
}