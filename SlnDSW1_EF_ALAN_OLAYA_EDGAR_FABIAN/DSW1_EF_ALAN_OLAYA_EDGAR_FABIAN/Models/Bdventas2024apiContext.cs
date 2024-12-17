using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DSW1_EF_ALAN_OLAYA_EDGAR_FABIAN.Models;

public partial class Bdventas2024apiContext : DbContext
{
    public Bdventas2024apiContext()
    {
    }

    public Bdventas2024apiContext(DbContextOptions<Bdventas2024apiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbArticulo> TbArticulos { get; set; }

    public virtual DbSet<TbArticulosBaja> TbArticulosBajas { get; set; }

    public virtual DbSet<TbArticulosLiquidacion> TbArticulosLiquidacions { get; set; }

    // Método para ejecutar el procedimiento almacenado
    public async Task<List<TbArticulo>> ListarArticulosActivos()
    {
        // Ejecuta el procedimiento almacenado sp_ListarArticulosActivos
        return await this.TbArticulos.FromSqlRaw("EXEC sp_ListarArticulosActivos").ToListAsync();
    }

    // Método para ejecutar el procedimiento almacenado sp_FiltrarArticuloActivoPorID
    public async Task<TbArticulo?> FiltrarArticuloActivoPorID(string codigoArticulo)
    {
        var parametro = new SqlParameter("@CodigoArticulo", codigoArticulo);

        // Ejecutamos el procedimiento almacenado y traemos los datos a memoria
        var result = await Task.Run(() =>
            this.TbArticulos
                .FromSqlRaw("EXEC sp_FiltrarArticuloActivoPorID @CodigoArticulo", parametro)
                .AsEnumerable()  // Trae los datos a memoria
                .FirstOrDefault()  // Aplica la operación en memoria
        );

        return result;
    }

    // Metodo para filtrar por iniciales
    public async Task<List<TbArticulo>> FiltrarArticulosActivosPorIniciales(string iniciales)
    {
        // Parámetro para las iniciales
        var paramIniciales = new SqlParameter("@Iniciales", iniciales ?? (object)DBNull.Value);

        // Ejecutar el procedimiento almacenado
        var resultados = await TbArticulos
            .FromSqlRaw("EXEC sp_FiltrarArticulosActivosPorIniciales @Iniciales", paramIniciales)
            .ToListAsync();

        return resultados;
    }

    //metodo para dar de baja
    public async Task<string> DarDeBajaArticulo(string codigoArticulo)
    {
        try
        {
            // Parámetro para el procedimiento almacenado
            var paramCodigo = new SqlParameter("@Codigo", codigoArticulo);

            // Ejecutar el procedimiento almacenado de forma asincrónica
            await Database.ExecuteSqlRawAsync("EXEC sp_DarDeBajaArticulo @Codigo", paramCodigo);

            // Si no hubo excepciones, devolver éxito
            return "Artículo dado de baja exitosamente.";
        }
        catch (SqlException ex)
        {
            // Capturar errores y devolver mensaje
            return $"Error al dar de baja el artículo: {ex.Message}";
        }
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=.;database=BDVENTAS2024API;integrated security=true;TrustServerCertificate=false;Encrypt=false;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AI");

        modelBuilder.Entity<TbArticulo>(entity =>
        {
            entity.HasKey(e => e.CodArt).HasName("pk_tb_articulos");

            entity.ToTable("tb_articulos");

            entity.Property(e => e.CodArt)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cod_art");
            entity.Property(e => e.DadoDeBaja)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValue("No")
                .IsFixedLength()
                .HasColumnName("dado_de_baja");
            entity.Property(e => e.NomArt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom_art");
            entity.Property(e => e.PreArt)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("pre_art");
            entity.Property(e => e.StkArt).HasColumnName("stk_art");
            entity.Property(e => e.UniMed)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("uni_med");
        });

        modelBuilder.Entity<TbArticulosBaja>(entity =>
        {
            entity.HasKey(e => new { e.CodArt, e.FechaBaja }).HasName("pk_tb_articulos_baja");

            entity.ToTable("tb_articulos_baja");

            entity.Property(e => e.CodArt)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cod_art");
            entity.Property(e => e.FechaBaja)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha_baja");

            entity.HasOne(d => d.CodArtNavigation).WithMany(p => p.TbArticulosBajas)
                .HasForeignKey(d => d.CodArt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_art_baja_cod_art");
        });

        modelBuilder.Entity<TbArticulosLiquidacion>(entity =>
        {
            entity.HasKey(e => e.NumReg).HasName("pk_tb_art_liquidacion");

            entity.ToTable("tb_articulos_liquidacion");

            entity.Property(e => e.NumReg).HasColumnName("num_reg");
            entity.Property(e => e.CodArt)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cod_art");
            entity.Property(e => e.PrecioLiquidar)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("precio_liquidar");
            entity.Property(e => e.UnidadesLiquidar).HasColumnName("unidades_liquidar");

            entity.HasOne(d => d.CodArtNavigation).WithMany(p => p.TbArticulosLiquidacions)
                .HasForeignKey(d => d.CodArt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_art_liqui_cod_art");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
