using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Reader: Entity
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Address { get; set; }

    public string? Info { get; set; }

    public virtual ICollection<ReadersBook> ReadersBooks { get; set; } = new List<ReadersBook>();
}
