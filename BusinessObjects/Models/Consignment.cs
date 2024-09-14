using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Consignment
{
    public int ConsignmentId { get; set; }

    public int AccountId { get; set; }

    public int KoiId { get; set; }

    public int PaymentId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool? IsDelete { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual KoiFish Koi { get; set; } = null!;
}
