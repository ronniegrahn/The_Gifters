using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace The_Gifters.Models.Entities;

public partial class GiftersContext : DbContext
{
    public GiftersContext(DbContextOptions<GiftersContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
