namespace DSW1_CLIENTE_EF_ALAN_OLAYA_EDGAR_FABIAN.Models
{
    public class TbArticulo
    {
        public string CodArt { get; set; } = "";
        public string? NomArt { get; set; } = "";
        public string? UniMed { get; set; } = "";
        public decimal? PreArt { get; set; }
        public int? StkArt { get; set; }
        public string? DadoDeBaja { get; set; } = "";
    }
}
