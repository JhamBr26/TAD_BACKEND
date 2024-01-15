namespace campground_api.Models.Dto
{
    public class ReviewDto
    {
        public int CampgroundId { get; set; }
        public string Body { get; set; } = "";
    }
}
