using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class KoiFish1
{
    public int KoiFishesId { get; set; }

    public int CategoryId { get; set; }

    public int Quantity { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
