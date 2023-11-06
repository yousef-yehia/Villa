using Villa_API.Data;
using Villa_API.Interfaces;
using Villa_API.Models;

namespace Villa_API.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly AppDbContext _appDbContext;
        public VillaNumberRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
        }

        public async Task<VillaNumber> Update(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _appDbContext.VillaNumbers.Update(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
