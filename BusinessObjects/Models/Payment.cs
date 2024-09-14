using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public DateTime? Date { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
