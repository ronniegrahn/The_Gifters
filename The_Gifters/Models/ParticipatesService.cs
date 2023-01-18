using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using The_Gifters.Controllers;
using The_Gifters.Models.Entities;
using The_Gifters.Views.Participations;

namespace The_Gifters.Models
{
    public class ParticipatesService
    {
        private readonly GiftersContext giftersContext;
        private readonly IHttpContextAccessor accessor;
        UserManager<IdentityUser> userManager;

        public ParticipatesService(GiftersContext giftersContext, IHttpContextAccessor accessor, UserManager<IdentityUser> userManager)
        {
            this.giftersContext = giftersContext;
            this.accessor = accessor;
            this.userManager = userManager;
        }

        public async Task AddParticipation(ParticipateVM participateVM)
        {
            await Task.Delay(0);

            DateTime dateTime = DateTime.Now;
            DateTime? participationEndDate = DateTime.Now;
            int? timeFrame = 0;

            int customerId = await GetCustomerIdAsync();

            var organizationName = participateVM.SelectedOrganizationName;
            var organization = await giftersContext.Organizations.FirstAsync(x => x.Id == organizationName);



            if (participateVM.IsRefundable)
            {
                participationEndDate.Value.AddMonths(12);
                timeFrame = 12;
            }
            else
            {
                participationEndDate = null;
                timeFrame = null;
            }


            await giftersContext.AddAsync(new Participation
            {
                Amount = participateVM.Amount,
                ParticipationDate = dateTime,
                OrganizationId = organization.Id,
                TimeFrame = timeFrame,
                ParticipationEndDate = participationEndDate,
                CustomerId = customerId, //Måste ändras till en variabel när användare är på plats
                IsRefundable = (timeFrame != null) ? true: false,
                IsActive= true,
            });

            await giftersContext.SaveChangesAsync();
        }


        public async Task<SelectListItem[]> GetOrganizationNamesAsync()
        {
            await Task.Delay(0);

            var organizationNames = new List<SelectListItem>();

            foreach (var organization in giftersContext.Organizations)
            {
                organizationNames.Add( 
					new SelectListItem { Value = organization.Id.ToString(), Text = organization.OrganizationName});
            };

			

            return organizationNames.ToArray();
		}

        public async Task<List<ParticipationVM>> GetMyParticipationsVMAsync()
        {
            await Task.Delay(0);

            int customerId = await GetCustomerIdAsync();

            List<ParticipationVM> participationVMs = new List<ParticipationVM>();


            var participations = giftersContext.Participations
                .Where(p => p.CustomerId == customerId)
                .ToList();

            foreach (var participation in participations)
            {
                participationVMs.Add(new ParticipationVM
                {
                    ParticipationAmount = Convert.ToDouble(participation.Amount),
                    StartDate = participation.ParticipationDate,
                    ContributionAmount = Convert.ToDouble(participation.SumGenerated),
                    OrganizationName = giftersContext.Organizations.First(x => x.Id == participation.OrganizationId).OrganizationName,
                    OrganizationDescription = giftersContext.Organizations.First(x => x.Id == participation.OrganizationId).Description,
                    ParticipationId = participation.Id,
                });
            }
            return participationVMs;
        }

        public async Task<DetailsVM> GetDetailsAsync(int participationId)
        {
            string tempOrg = "Red Cross";

            int customerId = await GetCustomerIdAsync();

            var participations2 = await giftersContext.Participations
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();

            var theParticipation = participations2.FirstOrDefault(x => x.Id == participationId);


            var detailsVM = new DetailsVM
            {
                ParticipationDate = theParticipation.ParticipationDate,
                ParticipationAmount = Convert.ToDouble(theParticipation.Amount),
                ParticipationEndDate = theParticipation.ParticipationEndDate,
                ParticipationTimeFrame = theParticipation.TimeFrame,
                ParticipationSumGenerated = Convert.ToDouble(theParticipation.SumGenerated),
                OrganizationName = giftersContext.Organizations.First(x => x.Id == theParticipation.OrganizationId).OrganizationName,
                OrganizationDescription = giftersContext.Organizations.First(x => x.Id == theParticipation.OrganizationId).Description,
            };

            return detailsVM;

            //var participations = giftersContext.Participations
            //    .Where(p => p.CustomerId == customerId && p.Id == participationId)
            //    .ToList();

            //// List<DetailsVM> detailsVM = new List<DetailsVM>();

            //var donationOrganisation = giftersContext.Participations
            //    .Where(p => p.CustomerId == customerId)
            //    .Join(giftersContext.Organizations, p => p.OrganizationId, o => o.Id, (p, o) => new { p, o })
            //    .Where(x => x.o.OrganizationName == tempOrg)
            //    .ToList();

            //foreach (var item in donationOrganisation)
            //{
            //detailsVM.Add(new DetailsVM
            //var detailsVM = new DetailsVM
            //{
            //    ParticipationDate = item.p.ParticipationDate,
            //    ParticipationAmount = Convert.ToDouble(item.p.Amount),
            //    ParticipationEndDate = item.p.ParticipationEndDate,
            //    ParticipationTimeFrame = item.p.TimeFrame,
            //    ParticipationSumGenerated = Convert.ToDouble(item.p.SumGenerated),
            //    OrganizationName = item.o.OrganizationName,
            //    OrganizationDescription = item.o.Description,
            //};
            //}

        }
        private async Task<int> GetCustomerIdAsync()
        {
            string userId = userManager.GetUserId(accessor.HttpContext.User);
            var user = await userManager.FindByIdAsync(userId);
            var customer = await giftersContext.Customers.FirstAsync(x => x.AspNetUsersId.Equals(userId));

            return customer.Id;
        }
    }
}
