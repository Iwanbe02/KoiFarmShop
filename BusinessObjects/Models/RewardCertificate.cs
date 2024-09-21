using BusinessObjects.Helpers;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class RewardCertificate : ISoftDelete
{
    public int Id { get; set; }

    public int? KoiId { get; set; }

    public int? OrderId { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual KoiFish? Koi { get; set; }
}
