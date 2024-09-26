using System;
using System.Collections.Generic;

namespace clients.Models;

public partial class ClientFile
{
    public int IdClientFile { get; set; }

    public int IdClient { get; set; }

    public string Filename { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;
}
