using System;
using System.Collections.Generic;

namespace Pitter.Models;

public partial class Follow
{
    public string IdFollower { get; set; } = null!;

    public string IdFollowing { get; set; } = null!;

    public virtual AspNetUser IdFollowerNavigation { get; set; } = null!;
}
