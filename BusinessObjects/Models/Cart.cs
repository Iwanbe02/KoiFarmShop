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

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Cartdetail> Cartdetails { get; set; } = new List<Cartdetail>();

    public virtual Order Order { get; set; } = null!;
}
