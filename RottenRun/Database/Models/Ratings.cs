﻿namespace DeliveryShop.Database.Models;

public class Ratings
{
    public int Id { get; set; }
    public Products Product { get; set; }
    public Users User { get; set; }
    public string Comment { get; set; }
}