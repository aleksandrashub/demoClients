using System;
using System.Collections.Generic;

namespace clients.Models;

public partial class Gender
{
    public int IdGender { get; set; }

    public string NameGender { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
