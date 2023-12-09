using System;
using System.Collections.Generic;

namespace Pitter.Models;

public partial class Post
{
    public long Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Date { get; set; }

    public long? ResponseToPostId { get; set; }

    public virtual ICollection<Repitt> Repitts { get; set; } = new List<Repitt>();

    public virtual AspNetUser User { get; set; } = null!;

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
