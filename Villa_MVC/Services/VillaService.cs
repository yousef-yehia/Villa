using MagicVilla_Web.Models;
using Villa_MVC.IServices;
using Villa_MVC.Models.Dto;

namespace Villa_MVC.Services
{
    public class VillaService : BaseService, IVillaService
    {
        new IHttpClientFactory _httpClientFactory;
        public string villaUrl = "https://localhost:7246";
        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:villaAPI")!;
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.POST,
                    Data = dto,
                    Url = villaUrl + "/api/villa"
                }

            );
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.DELETE,
                    Url = villaUrl + "/api/villa/" + id
                }

            );
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.GET,
                    Url = villaUrl + "/api/villa"
                }

            );
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.GET,
                    Url = villaUrl + "/api/villa/"+id
                }

            );
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.PUT,
                    Data = dto,
                    Url = villaUrl + "/api/villa/" + dto.Id
                }

            );
        }
    }
}
