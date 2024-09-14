using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int KoiId { get; set; }

    public int AccountId { get; set; }

    public int PaymentId { get; set; }

    public string Status { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int KoiFishesId { get; set; }

    public double Price { get; set; }

    public DateTime? Date { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual KoiFish Koi { get; set; } = null!;

    public virtual KoiFish1 KoiFishes { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
