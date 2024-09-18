using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Category : BaseEntity
{
    public int Id { get; set; }

    public string? Category1 { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();

    public virtual ICollection<KoiFishy> KoiFishies { get; set; } = new List<KoiFishy>();
}
