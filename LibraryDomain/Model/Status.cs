using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Status: Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReadersBook> ReadersBooks { get; set; } = new List<ReadersBook>();
}
