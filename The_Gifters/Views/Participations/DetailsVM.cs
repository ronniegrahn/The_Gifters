namespace The_Gifters.Views.Participations
{
    public class DetailsVM
    {
        public string OrganizationName { get; set; }
        public string OrganizationDescription { get; set; }
        public DateTime ParticipationDate { get; set; }
        public DateTime? ParticipationEndDate { get; set; }
        public double ParticipationAmount { get; internal set; }
        public int? ParticipationTimeFrame { get; internal set; }
        public double ParticipationSumGenerated { get; internal set; }
        public bool IsRefundable { get; set; }
        public bool IsActive { get; set; }
        public int ParticipationId { get; set; }
	}
}
