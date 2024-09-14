using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int AccountId { get; set; }

    public int OrderId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Cartdetail> Cartdetails { get; set; } = new List<Cartdetail>();

    public virtual Order Order { get; set; } = null!;
}
