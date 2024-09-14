using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Cartdetail
{
    public int CartDetailId { get; set; }

    public int KoiId { get; set; }

    public int CartId { get; set; }

    public int FeedbackId { get; set; }

    public int Quantity { get; set; }

    public double TotalPrice { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Feedback Feedback { get; set; } = null!;
}
