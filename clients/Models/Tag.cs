using System;
using System.Collections.Generic;

namespace clients.Models;

public partial class Tag
{
    public int IdTag { get; set; }

    public string? NameTag { get; set; }

    public string? ColorTag { get; set; }

    public virtual ICollection<Client> IdClients { get; set; } = new List<Client>();
}
