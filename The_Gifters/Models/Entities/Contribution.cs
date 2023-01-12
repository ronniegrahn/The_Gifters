using System;
using System.Collections.Generic;

namespace The_Gifters.Models.Entities;

public partial class Contribution
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public DateTime ContributionDate { get; set; }

    public int? TimeFrame { get; set; }

    public DateTime? ContributionEndDate { get; set; }

    public decimal? SumGenerated { get; set; }

    public int OrganizationId { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
