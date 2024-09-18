using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class RewardCertificate
{
    public int RewardcertificateId { get; set; }

    public int OrderId { get; set; }

    public int KoiId { get; set; }

    public string Desciptions { get; set; } = null!;

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual KoiFish Order { get; set; } = null!;
}
