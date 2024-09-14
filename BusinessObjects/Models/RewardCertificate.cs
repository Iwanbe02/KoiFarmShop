using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class RewardCertificate
{
    public int RewardcertificateId { get; set; }

    public int OrderId { get; set; }

    public int KoiId { get; set; }

    public string Desciptions { get; set; } = null!;

    public DateTime? Date { get; set; }

    public bool? IsDelete { get; set; }

    public virtual KoiFish Order { get; set; } = null!;
}
