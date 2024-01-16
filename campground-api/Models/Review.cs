using System;
using System.Collections.Generic;

namespace campground_api.Models;

public partial class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CampgroundId { get; set; }

    public string Body { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public int? Scoring { get; set; }

    public virtual Campground Campground { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
