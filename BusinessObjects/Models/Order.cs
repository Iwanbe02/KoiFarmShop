using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int KoiId { get; set; }

    public int AccountId { get; set; }

    public string Status { get; set; } = null!;

    public int PaymentId { get; set; }

    public string Type { get; set; } = null!;

    public int KoiFishes { get; set; }

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual KoiFish Koi { get; set; } = null!;

    public virtual KoiFish1 KoiFishesNavigation { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
