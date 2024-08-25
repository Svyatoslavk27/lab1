using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Book: Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Info { get; set; }

    public int CategoryId { get; set; }

    public virtual ICollection<AuthorsBook> AuthorsBooks { get; set; } = new List<AuthorsBook>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ReadersBook> ReadersBooks { get; set; } = new List<ReadersBook>();
}
