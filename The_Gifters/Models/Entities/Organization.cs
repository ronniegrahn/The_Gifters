using System;
using System.Collections.Generic;

namespace The_Gifters.Models.Entities;

public partial class Organization
{
    public int Id { get; set; }

    public string OrganizationName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Participation> Participations { get; } = new List<Participation>();
}
