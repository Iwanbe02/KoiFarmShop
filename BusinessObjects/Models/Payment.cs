using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
