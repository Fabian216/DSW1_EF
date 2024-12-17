using System;
using System.Collections.Generic;

namespace DSW1_EF_ALAN_OLAYA_EDGAR_FABIAN.Models;

public partial class TbArticulosLiquidacion
{
    public int NumReg { get; set; }

    public string CodArt { get; set; } = null!;

    public int UnidadesLiquidar { get; set; }

    public decimal PrecioLiquidar { get; set; }

    public virtual TbArticulo CodArtNavigation { get; set; } = null!;
}
