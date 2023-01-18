using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace The_Gifters.Views.Participations
{
	public class ParticipateVM
	{
		[Required(ErrorMessage = "Unable to donate 0")]
		[Range(0.01, double.MaxValue)]
		public decimal Amount { get; set; }
		public SelectListItem[]? OrganizationNames { get; set; }
		public int SelectedOrganizationName { get; set; }
		public bool IsRefundable { get; set; }
		public DateTime StartDate { get; set; } = DateTime.Now;
		public int UserId { get; set; }
	}
}
