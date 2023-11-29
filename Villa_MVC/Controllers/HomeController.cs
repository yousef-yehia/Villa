using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_MVC.IServices;
using Villa_MVC.Models;
using Villa_MVC.Models.Dto;

namespace Villa_MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly IVillaService _villaService;
		public HomeController(IVillaService villaService)
		{
			_villaService = villaService;
		}

		public async Task<IActionResult> Index()
		{
			List<VillaDTO> list = new();

			var response = await _villaService.GetAllAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}
	}
}