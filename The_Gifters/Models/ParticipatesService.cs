using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        public async void AddParticipation(ParticipateVM participateVM)
        {
            DateTime dateTime = DateTime.Now;
            DateTime? participationEndDate = DateTime.Now;
            int? timeFrame = 0;

            string organizationName = participateVM.OrganizationNames[0];
            var organization = giftersContext.Organizations.First(x => x.OrganizationName.Equals(organizationName));



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
                CustomerId = 1, //Måste ändras till en variabel när användare är på plats
            });

            giftersContext.SaveChanges();
        }

        public async Task<List<string>> GetOrganizationNamesAsync()
        {
            await Task.Delay(0);

            List<string> organizationNames = new List<string>();

            foreach (var organization in giftersContext.Organizations)
            {
                organizationNames.Add(organization.OrganizationName);
            };

            return organizationNames;
        }

        public async Task<List<ParticipationVM>> GetMyParticipationsVMAsync()
        {
            await Task.Delay(0);

            string userId = userManager.GetUserId(accessor.HttpContext.User);

            var user = await userManager.FindByIdAsync(userId);

            var customer = giftersContext.Customers.First(x => x.AspNetUsersId.Equals(userId));


            List<ParticipationVM> participationVMs = new List<ParticipationVM>();


            var participations = giftersContext.Participations
                .Where(p => p.CustomerId == customer.Id)
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
                });
            }
            return participationVMs;
        }

        public async Task<List<DetailsVM>> GetDetailsAsync()
        {
            string tempOrg = "Red Cross";



            string userId = userManager.GetUserId(accessor.HttpContext.User);
            var user = await userManager.FindByIdAsync(userId);
            var customer = giftersContext.Customers.First(x => x.AspNetUsersId.Equals(userId));



            List<DetailsVM> detailsVM = new List<DetailsVM>();



            var donationOrganisation = giftersContext.Participations
                .Where(p => p.CustomerId == customer.Id)
                .Join(giftersContext.Organizations, p => p.OrganizationId, o => o.Id, (p, o) => new { p, o })
                .Where(x => x.o.OrganizationName == tempOrg)
                .ToList();



            foreach (var item in donationOrganisation)
            {
                detailsVM.Add(new DetailsVM
                {
                    ParticipationDate = item.p.ParticipationDate,
                    ParticipationAmount = Convert.ToDouble(item.p.Amount),
                    ParticipationEndDate = item.p.ParticipationEndDate,
                    ParticipationTimeFrame = item.p.TimeFrame,
                    ParticipationSumGenerated = Convert.ToDouble(item.p.SumGenerated),
                    OrganizationName = item.o.OrganizationName,
                    OrganizationDescription = item.o.Description,
                });
            }

            return detailsVM;
        }
    }
}
