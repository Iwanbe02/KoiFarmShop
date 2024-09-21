using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Consignment
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int? KoiId { get; set; }

    public int? PaymentId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account? Account { get; set; }

    public virtual KoiFish? Koi { get; set; }

    public virtual Payment? Payment { get; set; }
}
