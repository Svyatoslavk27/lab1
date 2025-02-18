﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryDomain.Model;

public partial class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Поле не повинно бути порожнім")]
    [Display(Name="Категорія")]

    public string Name { get; set; } = null!;

    [Display(Name="Інформація про категорію")]

    public string? Info { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
