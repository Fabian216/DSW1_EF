using DSW1_CLIENTE_EF_ALAN_OLAYA_EDGAR_FABIAN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DSW1_CLIENTE_EF_ALAN_OLAYA_EDGAR_FABIAN.Controllers
{
    public class TbArticuloController : Controller
    {
        private readonly HttpClient _httpClient;

        public TbArticuloController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5195/api/"); // Reemplaza con la URL de tus APIs
        }

        // GET: TbArticuloController
        public async Task<IActionResult> IndexTbArticulo(string iniciales = "")
        {
            List<TbArticulo> articulos = new List<TbArticulo>();

            try
            {
                HttpResponseMessage response;

                if (string.IsNullOrEmpty(iniciales))
                {
                    // Llamar a la API GetArticulos si el TextBox está vacío
                    response = await _httpClient.GetAsync("ArticulosAPI/GetArticulos");
                }
                else
                {
                    // Llamar a la API FiltrarArticulosPorIniciales si hay texto en el TextBox
                    response = await _httpClient.GetAsync($"ArticulosAPI/FiltrarPorIniciales?iniciales={iniciales}");
                }

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    articulos = JsonConvert.DeserializeObject<List<TbArticulo>>(jsonString)!;
                }
                else
                {
                    ViewBag.Error = $"Error al obtener datos: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Excepción: {ex.Message}";
            }

            return View(articulos);
        }


        // GET: TbArticuloController/AgregarAlCarrito/5
        public async Task<IActionResult> DarDeBajaArticulo(string id)
        {
            TbArticulo articulo = null;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"ArticulosAPI/GetArticulo/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    articulo = JsonConvert.DeserializeObject<TbArticulo>(jsonString);
                }
                else
                {
                    ViewBag.Error = $"Error al obtener el artículo: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Excepción: {ex.Message}";
            }

            return View(articulo); // Pasa el artículo a la vista
        }

        // Método para dar de baja el artículo (usando PUT)
        [HttpPost]
        public async Task<IActionResult> DarDeBajaArticuloConfirmado(string id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsync($"ArticulosAPI/DarDeBaja/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    // Redirigir al listado de artículos o mostrar un mensaje de éxito
                    return RedirectToAction(nameof(IndexTbArticulo)); // O cualquier otra acción que prefieras
                }
                else
                {
                    ViewBag.Error = $"Error al dar de baja el artículo: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Excepción: {ex.Message}";
            }

            return View(); // Devolver a la vista actual en caso de error
        }



        // GET: TbArticuloController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TbArticuloController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TbArticuloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TbArticuloController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TbArticuloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TbArticuloController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TbArticuloController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
