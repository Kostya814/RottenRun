using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryShop.Database.Models;

public class Orders
{
    public int Id { get; set; }
    public Statuses Status { get; set; }
    public int UsersId { get; set; }
    public Users User { get; set; }
    public Addresses? DeliveryAddresses { get; set; }
    public DateTime? CreationDate { get; set; }
    public List<Baskets> BasketsList { get; set; } = new List<Baskets>();
    
    [NotMapped] 
    public double AllPrice { get; set; } = 0;


    public void CountPrice()
    {
        foreach (var basket in BasketsList)
        {
            AllPrice += basket.Product.Price;
        }

        AllPrice += 500;
    }

}