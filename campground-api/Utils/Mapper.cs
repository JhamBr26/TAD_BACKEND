using campground_api.Models;
using campground_api.Models.Dto;

namespace campground_api.Utils
{
    public static class Mapper
    {
        public static CampgroundGetDto MapCampgroundToCampgroundGetDto(Campground campground)
        {
            var campgroundGetDto = new CampgroundGetDto()
            {
                Id = campground.Id,
                Title = campground.Title,
                Description = campground.Description,
                Location = campground.Location,
                Price = campground.Price,
                Images = campground.Images.Select(image => new ImageDto()
                {
                    Filename = image.Filename,
                    Url = image.Url
                }).ToList(),
                Reviews = campground.Reviews.Select(review => new ReviewListDto()
                {
                    Id = review.Id,
                    Body = review.Body,
                    Scoring = review.Scoring,
                    User = new UserDto()
                    {
                        Id = review.User.Id,
                        Username = review.User.Username,
                        Email = review.User.Email,
                        FirstName = review.User.FirstName,
                        LastName = review.User.LastName
                    }
                }).ToList(),
                User = new UserDto()
                {
                    Id = campground.User.Id,
                    Username = campground.User.Username,
                    Email = campground.User.Email,
                    FirstName = campground.User.FirstName,
                    LastName = campground.User.LastName
                }
            };

            return campgroundGetDto;
        }

        public static CampgroundListDto MapCampgroundToCampgroundListDto(Campground campground)
        {
            var score = campground.Reviews.Select(review => review.Scoring).Average();
            var campgroundListDto = new CampgroundListDto()
            {
                Id = campground.Id,
                Title = campground.Title,
                Description = campground.Description,
                Location = campground.Location,
                Price = campground.Price,
                Images = campground.Images.Select(image => new ImageDto()
                {
                    Filename = image.Filename,
                    Url = image.Url
                }).ToList(),
                Score = score
            };

            return campgroundListDto;
        }

        public static ReviewListDto MapReviewToReviewListDto(Review review)
        {
            var reviewListDto = new ReviewListDto()
            {
                Id = review.Id,
                Body = review.Body,
                Scoring = review.Scoring,
                User = new UserDto()
                {
                    Id = review.User.Id,
                    Username = review.User.Username,
                    Email = review.User.Email,
                    FirstName = review.User.FirstName,
                    LastName = review.User.LastName
                }
            };

            return reviewListDto;
        }
    }
}
