using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool? IsDelete { get; set; }

    public virtual ICollection<KoiFish1> KoiFish1s { get; set; } = new List<KoiFish1>();

    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();
}
