namespace campground_api.Models.Dto
{
    public class CampgroundDto
    {
        public string Title { get; set; } = "";
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public List<ImageDto>? Images { get; set; }
    }
}
