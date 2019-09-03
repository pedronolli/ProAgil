using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangeAsync();
         Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes = false);
         Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false);
         Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes = false);
         Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos = false);
         Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos = false);

    }
}