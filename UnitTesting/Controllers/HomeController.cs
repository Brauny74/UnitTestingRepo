using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using UnitTesting.Models;
using UnitTesting.Interfaces;
using UnitTesting.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace UnitTesting.Controllers
{
	public class HomeController : Controller
	{
		private readonly IBrainStormSessionRepository _sessionRepository;

		public HomeController(IBrainStormSessionRepository sessionRepository)
		{
			_sessionRepository = sessionRepository;
		}

		public async Task<IActionResult> Index()
		{
			var sessionList = await _sessionRepository.ListAsync();

			var model = sessionList.Select(session => new StormSessionViewModel()
			{
				Id = session.Id,
				CreatedDate = session.CreatedDate,
				Name = session.Name,
				IdeaCount = session.Ideas.Count
			});

			return View(model);
		}

		public class NewSessionModel
		{
			[Required]
			public string SessionName { get; set; }
		}

		[HttpPost]
		public async Task<IActionResult> Index(NewSessionModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			else
			{
				await _sessionRepository.AddAsync(new BrainstormSession() { 
					CreatedDate = DateTime.Now,
					Name = model.SessionName
				});
			}

			return RedirectToAction(actionName : nameof(Index));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}