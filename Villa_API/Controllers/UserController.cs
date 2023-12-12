using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Villa_API.Interfaces;
using Villa_API.Models;
using Villa_API.Models.DTO;

namespace Villa_API.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._response = new APIResponse();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var logInResponse = await _userRepository.Login(loginRequestDTO);

            if (logInResponse == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("email or password is wrong");
                return BadRequest(_response);
            }
            _response.IsSuccess = true; 
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = logInResponse;
            return Ok(_response);
        }
        
        [HttpPost("Register")]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegisterationRequestDTO registerationRequestDTO)
        {
            bool UserUnique = _userRepository.UserExists(registerationRequestDTO.UserName);
            if (UserUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("userName is already registerd");
                return BadRequest(_response);

            }

            var registerResponse = await _userRepository.Register(registerationRequestDTO);

            if (registerResponse == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Something wrong happend");
                return BadRequest(_response);
            }
            _response.IsSuccess = true; 
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = registerResponse;
            return Ok(_response);
        }

    }
}
