using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class HealthCertificate
{
    public int HealthCertificateId { get; set; }

    public int KoiId { get; set; }

    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public DateTime CreateDate { get; set; }

    public int CreateBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime Deletedate { get; set; }

    public long DeleteBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual KoiFish Koi { get; set; } = null!;
}
