using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _proAgilContext;

        public ProAgilRepository(ProAgilContext proAgilContext)
        {
            _proAgilContext = proAgilContext;
           
            /*
             *  TRACKER AND NO TRACKER
             *  https://docs.microsoft.com/en-us/ef/core/querying/tracking
             */
            _proAgilContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void add<T>(T entity) where T : class
        {
            _proAgilContext.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _proAgilContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _proAgilContext.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _proAgilContext.SaveChangesAsync() > 0);
        }
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proAgilContext.Eventos
                    .Include(e => e.Lotes)
                    .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proAgilContext.Eventos
                   .Include(e => e.Lotes)
                   .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento)
                            .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proAgilContext.Eventos
                    .Include(e => e.Lotes)
                    .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                                .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento)
                            .Where(e => e.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proAgilContext.Palestrantes
                                        .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                            .ThenInclude(e => e.Evento);
            }

            query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proAgilContext.Palestrantes
                     .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                                .ThenInclude(e => e.Evento);
            }

            query = query.OrderByDescending(p => p.Nome)
                            .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }


    }
}