using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Villa_API.Interfaces;
using Villa_API.Models;
using Villa_API.Models.Dto;

namespace Villa_API.Controllers
{
    [Route("api/VillaNumber")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _dbVilla;
        protected APIResponse _response;



        public VillaNumberController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _dbVilla = dbVilla;
            this._response = new APIResponse();

        }

        [HttpGet(Name = "GetAllVillaNumber")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            IEnumerable<VillaNumber> villaNumbers = await _dbVillaNumber.GetAll(includeProperties: "Villa");
            _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumbers);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

        }

        [HttpGet("{villaNo:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VillaNumberDTO>> Get(int villaNo)
        {
            if (villaNo < 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;

                return BadRequest();
            }
            var villaNumber = await _dbVillaNumber.Get(V => V.VillaNo == villaNo , includeProperties: "Villa");
            if (villaNumber == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;

                return NotFound();
            }
            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost(Name = "createVillaNumber")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<APIResponse>> Create([FromBody] VillaNumberCreateDTO villaNocCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (villaNocCreateDto == null)
            {
                return BadRequest();
            }
            if (await _dbVilla.Get(u => u.Id == villaNocCreateDto.VillaID) == null)
            {
                ModelState.AddModelError("CustomError", "Villa ID is Invalid!");
                return BadRequest(ModelState);
            }
            if (await _dbVillaNumber.Get(v => v.VillaNo == villaNocCreateDto.VillaNo) != null)
            {
                ModelState.AddModelError("ErrorMessages", "Villa Number already Exists!");
                return BadRequest(ModelState);
            }

            var villaNoToCreate = _mapper.Map<VillaNumber>(villaNocCreateDto);
            await _dbVillaNumber.Create(villaNoToCreate);
            _response.Result = _mapper.Map<VillaNumberDTO>(villaNoToCreate);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetVilla", new { id = villaNoToCreate.VillaNo }, _response);

        }

        [HttpDelete("{villaNo:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Delete(int villaNo)
        {
            var villaNoToDelete = await _dbVillaNumber.Get(v => v.VillaNo == villaNo);

            if (villaNoToDelete == null)
            {
                return NotFound();
            }
            await _dbVillaNumber.Delete(villaNoToDelete);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpPut("{villaNo:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int villaNo, [FromBody] VillaNumberUpdateDTO villaNoUpdateDTO)
        {
            {
                if (villaNoUpdateDTO == null || villaNo != villaNoUpdateDTO.VillaNo)
                {
                    return BadRequest();
                }
                if (await _dbVilla.Get(u => u.Id == villaNoUpdateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }
                var villa = _mapper.Map<VillaNumber>(villaNoUpdateDTO);

                await _dbVillaNumber.Update(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);


            }
        }
    }
}
