using MagicVilla_Web.Models;
using Villa_MVC.IServices;
using Villa_MVC.Models.Dto;

namespace Villa_MVC.Services
{
    public class VillaNumberService :  BaseService , IVillaNumberService
    {
        IHttpClientFactory _httpClientFactory;
        public readonly string villaUrl;

        public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            this.villaUrl = configuration.GetValue<string>("ServiceUrls:villaAPI")!;
        }

        public Task<T> Create<T>(VillaNumberCreateDTO dto)
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.POST,
                    Data = dto,
                    Url = villaUrl + "/api/VillaNumber"
				}

                );
        }
        public Task<T> Delete<T>(int id)
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.DELETE,
                    Url = villaUrl + "/api/VillaNumber/" + id
                }

            );
        }

        public Task<T> GetAll<T>()
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.GET,
                    Url = villaUrl + "/api/VillaNumber"
                }

            );
        }

        public Task<T> Get<T>(int id)
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.GET,
                    Url = villaUrl + "/api/VillaNumber/" + id
                }

            );
        }

        public Task<T> Update<T>(VillaNumberUpdateDTO dto)
        {
            return SendAsync<T>(
                new APIRequest()
                {
                    ApiType = Models.APIType.PUT,
                    Data = dto,
                    Url = villaUrl + "/api/VillaNumber/" + dto.VillaID
                }

            );
        }

    }
}
