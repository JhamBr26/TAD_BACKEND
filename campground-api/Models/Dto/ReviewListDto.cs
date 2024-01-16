namespace campground_api.Models.Dto
{
    public class ReviewListDto
    {
        public int Id { get; set; }
        public string Body { get; set; } = "";
        public int? Scoring { get; set; }
        public UserDto User { get; set; } = new UserDto();
    }
}
