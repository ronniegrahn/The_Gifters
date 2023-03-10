using System.ComponentModel.DataAnnotations;

namespace The_Gifters.Views.Participations
{
	public class MyParticipationsVM
	{
		public List<ParticipationVM> Participations { get; set; }
		public double RunningTotalAll { get; set; }
		public double RunningTotalUser { get; set; }
	}

	public class ParticipationVM
	{
		public string OrganizationName { get; set; }
		public string OrganizationDescription { get; set; }
		public double ParticipationAmount { get; set; }
		public double ContributionAmount { get; set; }
		public DateTime StartDate { get; set; }
		public int ParticipationId { get; set; }
	}
}
