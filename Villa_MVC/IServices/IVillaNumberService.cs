using Villa_MVC.Models.Dto;

namespace Villa_MVC.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAll<T>();
        Task<T> Get<T>(int id);
        Task<T> Create<T>(VillaNumberCreateDTO dto);
        Task<T> Update<T>(VillaNumberUpdateDTO dto);
        Task<T> Delete<T>(int id);
    }
}
