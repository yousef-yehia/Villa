using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_MVC.IServices;
using Villa_MVC.Models;
using Villa_MVC.Models.Dto;
using Villa_MVC.Services;

namespace Villa_MVC.Controllers
{
    public class VillaNumberController : Controller
    { 
        public readonly IVillaNumberService _vnumberService;
        public readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService vnumberService, IMapper mapper)
        {
            _vnumberService = vnumberService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllVillaNumbers() 
        {
            List<VillaNumberDTO> villaNumbers = new List<VillaNumberDTO>();

            var response = await _vnumberService.GetAll<APIResponse>();
            if (response == null) 
            {
                return View(villaNumbers);
            }

            villaNumbers = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            return View(villaNumbers);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _vnumberService.Create<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(GetAllVillaNumbers));
                }
            }
            return View(model);
        }


        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            var response = await _vnumberService.Get<APIResponse>(villaNo);
            if (response != null)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaNumberUpdateDTO>(model));
            }
            if(response == null) 
            {
				return NotFound();
			}
			return View();

		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _vnumberService.Update<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(GetAllVillaNumbers));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            var response = await _vnumberService.Get<APIResponse>(villaNo);
            if (response != null)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            if (response == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO villa)
        {


            var response = await _vnumberService.Delete<APIResponse>(villa.VillaNo);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(GetAllVillaNumbers));
            }

            return View(villa);
        }
    }
}
