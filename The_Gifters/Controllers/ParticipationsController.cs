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


        #if !DEBUG
        [Authorize]
        #endif
        [HttpGet("participations/myparticipations")]
        public IActionResult MyParticipations()
        {
            return View();
        }

        #if !DEBUG
        [Authorize]
        #endif
        [HttpGet("participations/details")]
        public IActionResult Details()
        {
            return View();
        }

        [HttpPost("participations/participate")]
        public IActionResult CreateParticipation(ParticipateVM participateVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var buttonName = Request.Form["Refundable"].Count > 0 ? "Refundable" : "Donate";

            if (buttonName == "Refundable")
                participateVM.IsRefundable = true;
            else
                participateVM.IsRefundable = false;

            participatesService.AddParticipation(participateVM);
            return RedirectToAction(nameof(MyParticipations));
        }
    }
}
