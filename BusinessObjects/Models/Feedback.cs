using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string Status { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Rating { get; set; }

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Cartdetail> Cartdetails { get; set; } = new List<Cartdetail>();
}
