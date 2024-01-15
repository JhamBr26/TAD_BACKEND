using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace campground_api.Models;

public partial class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CampgroundId { get; set; }

    public string Body { get; set; } = null!;
    [JsonIgnore]
    public virtual Campground Campground { get; set; } = null!;
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
