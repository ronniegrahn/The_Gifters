namespace The_Gifters.Views.Participations
{
	public class DetailsVM
	{
		public string OrganizationName { get; set; }
		public string OrganizationDescription { get; set; }
		public double ContributionAmount { get; set; }
		public DateTime StartDate { get; set; }
		public bool IsRefundable { get; set; }
	}
}
