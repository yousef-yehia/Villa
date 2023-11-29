using AutoMapper.Internal;
using MagicVilla_Web.Models;

namespace Villa_MVC.IServices
{
    public interface IBaseService
    {
        Task<T> SendAsync<T>(APIRequest apiRequest);

    }
}
