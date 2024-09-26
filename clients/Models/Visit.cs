using System;
using System.Collections.Generic;

namespace clients.Models;

public partial class Visit
{
    public int IdClientVisit { get; set; }

    public int IdClient { get; set; }

    public DateTime TimedateVisit { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;
}
