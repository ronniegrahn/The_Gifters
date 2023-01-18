﻿using Microsoft.AspNetCore.Http;
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
                IsRefundable = (timeFrame != null) ? true : false,
                IsActive = true,
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
                    new SelectListItem { Value = organization.Id.ToString(), Text = organization.OrganizationName });
            };



            return organizationNames.ToArray();
        }

		public async Task<double> RunningTotal()
		{

			List<Participation> AllParticipations = new List<Participation>();

			foreach (var item in giftersContext.Participations)
			{
				AllParticipations.Add(item);
			}

			var runningTotal = AllParticipations.Sum(x => x.Amount);
			double theRunningTotal = 0;

			foreach (var item in AllParticipations)
			{
				DateTime? participationEndDate = DateTime.Now;
				if (!item.IsActive)
				{
					participationEndDate = item.ParticipationEndDate;
				}

				theRunningTotal += CalculateInterest(item.ParticipationDate, participationEndDate, Convert.ToDouble(item.Amount), 0.05);
			}

			return theRunningTotal;
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
                    ContributionAmount = Math.Round(CalculateInterest(participation.ParticipationDate, DateTime.Now, Convert.ToDouble(participation.Amount), 0.05)),
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
                ParticipationSumGenerated = Math.Round(CalculateInterest(theParticipation.ParticipationDate, DateTime.Now, Convert.ToDouble(theParticipation.Amount), 0.05)),
                OrganizationName = giftersContext.Organizations.First(x => x.Id == theParticipation.OrganizationId).OrganizationName,
                OrganizationDescription = giftersContext.Organizations.First(x => x.Id == theParticipation.OrganizationId).Description,
            };

            return detailsVM;

        }
        private async Task<int> GetCustomerIdAsync()
        {
            string userId = userManager.GetUserId(accessor.HttpContext.User);
            var user = await userManager.FindByIdAsync(userId);
            var customer = await giftersContext.Customers.FirstAsync(x => x.AspNetUsersId.Equals(userId));

            return customer.Id;
        }

        private double CalculateInterest(DateTime? startDate, DateTime? endDate, double initialAmount, double interestRate)
        {
            TimeSpan? difference = endDate - startDate;
            double days = difference.Value.TotalDays;
            double years = days / 365.25;
            double interest = initialAmount * interestRate * years;
            return interest;
        }

    }
}
