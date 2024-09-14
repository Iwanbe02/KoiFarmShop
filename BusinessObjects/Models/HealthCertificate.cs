﻿using System;
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

    public bool? IsDelete { get; set; }

    public virtual KoiFish Koi { get; set; } = null!;
}
