using DSW1_EF_ALAN_OLAYA_EDGAR_FABIAN.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSW1_EF_ALAN_OLAYA_EDGAR_FABIAN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosAPIController : ControllerBase
    {

        private readonly Bdventas2024apiContext ctx;

        public ArticulosAPIController(Bdventas2024apiContext _ctx)
        {
            ctx = _ctx;
        }

        // GET: api/<ArticulosAPIController>
        [HttpGet("GetArticulos")]
        public async Task<List<TbArticulo>> GetArticulos()
        {
            // Llamamos al procedimiento almacenado para listar los artículos activos
            var listado = await ctx.ListarArticulosActivos();
            return listado;
        }



        // GET: api/<ArticulosAPIController>/5
        [HttpGet("GetArticulo/{id}")]
        public IActionResult GetArticulo(string id)
        {
            // Llamamos al método FiltrarArticuloActivoPorID
            var articulo = ctx.FiltrarArticuloActivoPorID(id);

            // Verificamos si encontramos el artículo
            if (articulo == null)
            {
                return NotFound();  // Si no se encuentra, devolvemos un error 404
            }

            // Si se encuentra, devolvemos el artículo
            return Ok(articulo);
        }

        // GET: api/ArticulosAPIController/FiltrarPorIniciales?iniciales=A
        [HttpGet("FiltrarPorIniciales")]
        public IActionResult FiltrarArticulosPorIniciales([FromQuery] string iniciales)
        {
            // Llamamos al método del contexto para filtrar los artículos
            var articulos = ctx.FiltrarArticulosActivosPorIniciales(iniciales);

            // Si no se encuentran resultados, devolver 404
            if (articulos == null || articulos.Count == 0)
            {
                return NotFound("No se encontraron artículos con esas iniciales.");
            }

            // Devolver los artículos encontrados
            return Ok(articulos);
        }

        [HttpPut("DarDeBaja/{codigo}")]
        public IActionResult DarDeBajaArticulo([FromRoute] string codigo)
        {
            // Llamar al método del contexto para dar de baja el artículo
            var resultado = ctx.DarDeBajaArticulo(codigo);

            // Verificar si el procedimiento fue exitoso o tuvo errores
            if (resultado.Contains("Error"))
            {
                return BadRequest(resultado); // Error en la operación
            }

            return Ok(resultado); // Éxito en la operación
        }




        // POST api/<ArticulosAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArticulosAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticulosAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
