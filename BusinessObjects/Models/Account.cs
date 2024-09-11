using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime Date { get; set; }

    public int Point { get; set; }

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
