namespace The_Gifters.Views.Contributions
{
	public class MyParticipationsVM
	{
		public List<ParticipationVM> Participations { get; set; }
		public decimal RunningTotal { get; set; }
	}

	public class ParticipationVM
	{
		public string OrganizationName { get; set; }
		public string OrganizationDescription { get; set; }
		public double ParticipationAmount { get; set; }
		public double ContributionAmount { get; set; }
		public DateTime StartDate { get; set; }
	}
}

