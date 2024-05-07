namespace DeliveryShop.Database.Models;

public class Addresses
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Home { get; set; }
    public int ApartmentNumber { get; set; }
    public List<Baskets> ListBasket { get; set; } = new List<Baskets>();
    public List<Orders> OrdersList { get; set; } = new List<Orders>();
    public string GetAddress => "г."+City + " ул." + Street + " д." + Home + " кв." +ApartmentNumber;
}