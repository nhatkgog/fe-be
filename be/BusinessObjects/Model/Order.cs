using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public int LaptopId { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

    public DateTime OrderDate { get; set; }
    [JsonIgnore]
    public virtual Laptop Laptop { get; set; } = null!;
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
