using System;
using System.Collections.Generic;

namespace DSW1_EF_ALAN_OLAYA_EDGAR_FABIAN.Models;

public partial class TbArticulosBaja
{
    public string CodArt { get; set; } = null!;

    public DateOnly FechaBaja { get; set; }

    public virtual TbArticulo CodArtNavigation { get; set; } = null!;
}
