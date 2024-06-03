using ApiPracticaEventos.Models;
using ApiPracticaEventos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPracticaEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private EventosRepository repo;
        public EventosController(EventosRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> GetEventos()
        {
            return await this.repo.GetEventosAsync();
        }
        [HttpGet("{idcategoria}")]
        public async Task<ActionResult<List<Evento>>> GetEventos(int idcategoria)
        {
            return await this.repo.GetEventosByCategoria(idcategoria);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<CategoriaEvento>>> GetCategorias()
        {
            return await this.repo.GetCategoriasAsync();
        }
    }
}
