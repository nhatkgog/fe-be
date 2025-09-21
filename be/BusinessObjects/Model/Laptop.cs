using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Model;

public partial class Laptop
{
    public int LaptopId { get; set; }

    public string Name { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
