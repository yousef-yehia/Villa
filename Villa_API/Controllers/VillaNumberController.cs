using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Villa_API.Interfaces;
using Villa_API.Models;
using Villa_API.Models.Dto;

namespace Villa_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _dbVilla;


        public VillaNumberController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _dbVilla = dbVilla;
        }

        [HttpGet(Name = "GetAllVillaNumber")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaNumberDTO>>> GetAll()
        {
            IEnumerable<VillaNumber> villaNumbers = await _dbVillaNumber.GetAll();
            return Ok(_mapper.Map<List<VillaNumberDTO>>(villaNumbers));

        }

        [HttpGet("villaNo:int", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VillaNumberDTO>> Get(int villaNo)
        {
            if (villaNo < 0)
            {
                return BadRequest();
            }
            var villaNumber = await _dbVillaNumber.Get(V => V.VillaNo == villaNo);
            if (villaNumber == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaNumberDTO>(villaNumber));
        }

        [HttpPost(Name = "createVillaNumber")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<VillaNumberDTO>> Create([FromBody] VillaNumberCreateDTO villaNocCreateDto)
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

            return CreatedAtRoute("GetVillaNumber", new { id = villaNoToCreate.VillaNo }, villaNocCreateDto);

        }

        [HttpDelete("{villaNo:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int villaNo)
        {
            var villaNoToDelete = await _dbVillaNumber.Get(v => v.VillaNo == villaNo);

            if (villaNoToDelete == null)
            {
                return NotFound();
            }
            await _dbVillaNumber.Delete(villaNoToDelete);
            return NoContent();

        }

        [HttpPut("{villaNo:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int villaNo, [FromBody] VillaNumberUpdateDTO villaNoUpdateDTO)
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

                return NoContent();

            }
        }
    }
}
