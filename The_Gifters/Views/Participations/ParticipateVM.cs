namespace The_Gifters.Views.Contributions
{
	public class ParticipateVM
	{
		public int Amount { get; set; }
		public List<string> OrganizationNames { get; set; }
		public bool IsRefundable { get; set; }
		public DateTime StartDate { get; set; }
		public int UserId { get; set; }
	}
}
