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

		public ParticipatesService(GiftersContext giftersContext)
		{
			this.giftersContext = giftersContext;
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
	}
}
