using Microsoft.AspNetCore.Mvc.Rendering;

namespace The_Gifters.Views.Participations
{
	public class ParticipateVM
	{
		public decimal Amount { get; set; }
		public SelectListItem[]? OrganizationNames { get; set; }
		public int SelectedOrganizationName { get; set; }
		public bool IsRefundable { get; set; }
		public DateTime StartDate { get; set; } = DateTime.Now;
		public int UserId { get; set; }
	}
}
