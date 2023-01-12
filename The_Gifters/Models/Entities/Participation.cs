using System;
using System.Collections.Generic;

namespace The_Gifters.Models.Entities;

public partial class Participation
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime ParticipationDate { get; set; }

    public int? TimeFrame { get; set; }

    public DateTime? ParticipationEndDate { get; set; }

    public decimal? SumGenerated { get; set; }

    public int OrganizationId { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
