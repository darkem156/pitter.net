using System;
using System.Collections.Generic;

namespace Pitter.Models;

public partial class Repitt
{
    public string UserId { get; set; } = null!;

    public long PostId { get; set; }

    public virtual Post Post { get; set; } = null!;
}
