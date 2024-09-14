using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public int AccountId { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
