using Microsoft.AspNetCore.Mvc;
using UnitTesting.Interfaces;
using UnitTesting.ViewModels;

namespace UnitTesting.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBrainStormSessionRepository _sessionRepository;

        public SessionController(IBrainStormSessionRepository sessionRepository)
        { 
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!id.HasValue)
            { 
                return RedirectToAction(actionName : nameof(Index), controllerName: "Home");
            }

            var session = await _sessionRepository.GetByIdAsync(id.Value);
            if(session == null)
            {
                return Content("Session not found.");
            }

            var viewModel = new StormSessionViewModel()
            {
                CreatedDate = session.CreatedDate,
                Name = session.Name,
                Id = session.Id
            };

            return View(viewModel);
        }
    }
}
