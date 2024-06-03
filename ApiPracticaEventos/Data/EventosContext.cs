using ApiPracticaEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPracticaEventos.Data
{
    public class EventosContext:DbContext
    {
        public EventosContext(DbContextOptions<EventosContext>options):base(options)
        {
            
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<CategoriaEvento> Categorias { get; set; }
    }
}
