using System;
using System.Collections.Generic;

namespace DSW1_EF_ALAN_OLAYA_EDGAR_FABIAN.Models;

public partial class TbArticulo
{
    public string CodArt { get; set; } = null!;

    public string? NomArt { get; set; }

    public string? UniMed { get; set; }

    public decimal? PreArt { get; set; }

    public int? StkArt { get; set; }

    public string? DadoDeBaja { get; set; }

    public virtual ICollection<TbArticulosBaja> TbArticulosBajas { get; set; } = new List<TbArticulosBaja>();

    public virtual ICollection<TbArticulosLiquidacion> TbArticulosLiquidacions { get; set; } = new List<TbArticulosLiquidacion>();
}
