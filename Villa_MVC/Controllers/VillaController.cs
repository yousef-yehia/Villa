using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_MVC.IServices;
using Villa_MVC.Models;
using Villa_MVC.Models.Dto;
using Villa_MVC.Services;

namespace Villa_MVC.Controllers
{
    public class VillaController : Controller
    {
        public readonly IVillaService _VillaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _VillaService = villaService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllVillas()
        {
            List<VillaDTO> villas = new List<VillaDTO>();
            var response = await _VillaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villas = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(villas);
        }

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _VillaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(GetAllVillas));
                }
            }
            return View(model);
        }


        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _VillaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaUpdateDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _VillaService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(GetAllVillas));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _VillaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess)
            {
                VillaDTO villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(villa);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO villa)
        {


            var response = await _VillaService.DeleteAsync<APIResponse>(villa.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(GetAllVillas));
            }

            return View(villa);
        }
    }
}
