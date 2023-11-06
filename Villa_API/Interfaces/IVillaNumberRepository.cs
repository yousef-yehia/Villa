using System.Linq.Expressions;
using Villa_API.Models;

namespace Villa_API.Interfaces
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> Update(VillaNumber entity);

    }
}
