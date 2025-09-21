using System;
using System.Collections.Generic;

namespace BusinessObjects.Model;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
