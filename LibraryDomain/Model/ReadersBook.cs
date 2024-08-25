using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class ReadersBook: Entity
{
    public int Id { get; set; }

    public int ReaderId { get; set; }

    public int BookId { get; set; }

    public DateOnly Issue { get; set; }

    public DateOnly PlanReturn { get; set; }

    public int StatusId { get; set; }

    public DateOnly? FactReturn { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
