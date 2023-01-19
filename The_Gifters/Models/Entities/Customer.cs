using System;
using System.Collections.Generic;

namespace The_Gifters.Models.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string AspNetUsersId { get; set; } = null!;

    public virtual AspNetUser AspNetUsers { get; set; } = null!;

    public virtual ICollection<Participation> Participations { get; } = new List<Participation>();
}
