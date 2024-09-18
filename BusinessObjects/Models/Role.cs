using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
