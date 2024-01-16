namespace campground_api.Models.Dto
{
    public class ReviewCreateDto
    {
        public int CampgroundId { get; set; }
        public string Body { get; set; } = "";
        public int Scoring { get; set; }
    }
}
