using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Villa_API.Data;
using Villa_API.Interfaces;
using Villa_API.Models;
using Villa_API.Models.Dto;

namespace Villa_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaRepository _dbVilla;
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IMapper _mapper;

        public VillaAPIController(IVillaRepository dbVilla, ILogger<VillaAPIController> logger, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");
            IEnumerable<Villa> villaList = await _dbVilla.GetAll();
            return Ok( _mapper.Map<List<VillaDTO>>(villaList) );
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get Villa Error with Id" + id);
                return BadRequest();
            }

            var villa = await _dbVilla.Get(V => V.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest();
            }
            if (await _dbVilla.Get(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null) 
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }

            Villa villa = _mapper.Map<Villa>(villaDto);


            await _dbVilla.Create(villa);


            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villaDto);
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> DeleteVilla(int id)
        {
            var villa = await _dbVilla.Get(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            await _dbVilla.Delete(villa);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
        {
            {
                if (villaDTO == null || id != villaDTO.Id)
                {
                    return BadRequest();
                }

                Villa villa = _mapper.Map<Villa>(villaDTO);

                await _dbVilla.Update(villa);

                return NoContent();

            }
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVilla.Get(u => u.Id == id, tracked: false);
            if (villa == null)
            {
                return BadRequest();
            }
            //patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            VillaUpdateDTO villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null)
            {
                return BadRequest();
            }


            patchDTO.ApplyTo(villaUpdateDTO, ModelState);

            Villa villa1 = _mapper.Map<Villa>(villaUpdateDTO);
           
            await _dbVilla.Update(villa1);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

