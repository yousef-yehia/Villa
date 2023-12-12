using Villa_API.Models;
using Villa_API.Models.DTO;

namespace Villa_API.Interfaces
{
    public interface IUserRepository
    {
        bool UserExists(string userName);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
