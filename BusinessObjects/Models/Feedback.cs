using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string Status { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Rating { get; set; }

    public DateTime? Date { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Cartdetail> Cartdetails { get; set; } = new List<Cartdetail>();
}
