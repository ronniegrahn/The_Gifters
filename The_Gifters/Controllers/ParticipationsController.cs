using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using The_Gifters.Models;
using The_Gifters.Models.Entities;
using The_Gifters.Views.Participations;

namespace The_Gifters.Controllers
{
    public class ParticipationsController : Controller
    {
        private readonly ParticipatesService participatesService;

        public ParticipationsController(ParticipatesService participatesService)
        {
            this.participatesService = participatesService;
        }

#if !DEBUG
        [Authorize]
#endif
        [HttpGet("participations/participate")]
        public async Task<IActionResult> ParticipateAsync()
        {
            var organizationNames = await participatesService.GetOrganizationNamesAsync();

            ParticipateVM model = new ParticipateVM()
            {
                OrganizationNames = organizationNames,
            };

            return View(model);
        }

        [Authorize]

        [HttpGet("participations/myparticipations/")]
        public async Task<IActionResult> MyParticipationsAsync()
        {
            var myParticipationsVM = await participatesService.GetMyParticipationsVMAsync();
			var runningTotal = await participatesService.RunningTotal();

            var model = new MyParticipationsVM()
            {
                Participations = myParticipationsVM,
                RunningTotal = Math.Round(runningTotal),
            };
            return View(model);
        }

#if !DEBUG
        [Authorize]
#endif
        [HttpGet("participations/details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var model = await participatesService.GetDetailsAsync(id);

            return View(model);
        }

        [HttpPost("participations/participate")]
        public async Task<IActionResult> CreateParticipation(ParticipateVM participateVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("participate", "Participations");
            }

            var buttonName = Request.Form["Refundable"].Count > 0 ? "Refundable" : "Donate";

            if (buttonName == "Refundable")
                participateVM.IsRefundable = true;
            else
                participateVM.IsRefundable = false;

            await participatesService.AddParticipation(participateVM);
            return RedirectToAction("MyParticipations", "Participations");

        }

		[HttpPost("participations/details/{id}")]
		public async Task<IActionResult> DeleteParticipation(int id)
		{
			await participatesService.DeleteParticipation(id);
			return RedirectToAction("MyParticipations", "Participations");
		}
	}
}
