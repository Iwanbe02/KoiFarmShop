using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class KoiFish1
{
    public int KoiFishesId { get; set; }

    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
