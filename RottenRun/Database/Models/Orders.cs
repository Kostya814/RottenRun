namespace DeliveryShop.Database.Models;

public class Orders
{
    public int Id { get; set; }
    public Statuses Status { get; set; }
    public int UsersId { get; set; }
    public Users User { get; set; }
    public Addresses? DeliveryAddresses { get; set; }
    public List<Baskets> BasketsList { get; set; } = new List<Baskets>();
}