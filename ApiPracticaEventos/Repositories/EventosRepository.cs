using ApiPracticaEventos.Data;
using ApiPracticaEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPracticaEventos.Repositories
{
    public class EventosRepository
    {
        private EventosContext context;
        public EventosRepository(EventosContext context)
        {
            this.context = context;
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            return await this.context.Eventos.ToListAsync();
        }

        public async Task<List<Evento>> GetEventosByCategoria(int id)
        {
            return await this.context.Eventos
                .Where(x=>x.IdCategoria==id).ToListAsync();
        }

        public async Task<List<CategoriaEvento>> GetCategoriasAsync()
        {
            return await this.context.Categorias.ToListAsync();
        }
    }
}
