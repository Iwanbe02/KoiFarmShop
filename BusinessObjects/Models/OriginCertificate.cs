using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class OriginCertificate
{
    public int OriginCertificateId { get; set; }

    public int KoiId { get; set; }

    public int OrderId { get; set; }

    public string Origin { get; set; } = null!;

    public DateTime? Date { get; set; }

    public bool? IsDelete { get; set; }

    public virtual KoiFish Koi { get; set; } = null!;
}
