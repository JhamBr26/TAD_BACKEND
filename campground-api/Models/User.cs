using System;
using System.Collections.Generic;

namespace campground_api.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? Salt { get; set; }

    public string? Hash { get; set; }

    public virtual ICollection<Campground> Campgrounds { get; set; } = new List<Campground>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
