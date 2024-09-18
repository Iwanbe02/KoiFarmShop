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

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Feedback Feedback { get; set; } = null!;
}
