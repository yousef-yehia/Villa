using System.Linq.Expressions;
using Villa_API.Models;

namespace Villa_API.Interfaces
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> Update(Villa entity);

    }
}
