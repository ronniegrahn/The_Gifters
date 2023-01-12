namespace The_Gifters.Views.Contributions
{
	public class ParticipateVM
	{
		public decimal Amount { get; set; }
		public List<string> OrganizationNames { get; set; }
		public bool IsRefundable { get; set; }
		public DateTime StartDate { get; set; } = DateTime.Now;
		public int UserId { get; set; }
	}
}
