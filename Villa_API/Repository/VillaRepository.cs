using Villa_API.Data;
using Villa_API.Interfaces;
using Villa_API.Models;

namespace Villa_API.Repository
{
    public class VillaRepository : Repository<Villa> ,IVillaRepository
    {
        private readonly AppDbContext _appDb;

        public VillaRepository(AppDbContext appDb) : base(appDb)
        {
            _appDb = appDb;
        }

        public async Task<Villa> Update(Villa villa)
        {
            _appDb.Villas.Update(villa);
            await _appDb.SaveChangesAsync();
            return villa;
        }
    }
}
