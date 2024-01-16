using campground_api.Models;
using campground_api.Models.Dto;
using campground_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace campground_api.Services
{
    public class ReviewService
    {
        private readonly CampgroundContext _context;
        public ReviewService(CampgroundContext context)
        {
            _context = context;
        }

        public async Task<List<ReviewListDto>> GetByCampgroundId(int id)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.CampgroundId == id)
                .ToListAsync();

            return reviews.Select(Mapper.MapReviewToReviewListDto).ToList();
        }

        public async Task<ReviewCreateDto> Create(int userID, ReviewCreateDto reviewDto)
        {
            _context.Reviews.Add(new Review()
            {
                UserId = userID,
                CampgroundId = reviewDto.CampgroundId,
                Body = reviewDto.Body,
                Scoring = reviewDto.Scoring,
                CreateAt = DateTime.UtcNow,
            });
            await _context.SaveChangesAsync();
            return reviewDto;
        }

        public async Task<Review?> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return null;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return review;
        }
        public async Task<Review?> Update(int id, ReviewCreateDto reviewDto)
        {

            try
            {
                var review = await _context.Reviews.FindAsync(id);
                if(review is null) return null;

                review.Body = reviewDto.Body;
                review.Scoring = reviewDto.Scoring;

                await _context.SaveChangesAsync();

                return review;
            }
            catch (DbUpdateConcurrencyException) when (!ReviewExists(id))
            {
                return null;
            }
        }

        private bool ReviewExists(int id) =>
            _context.Reviews.Any(e => e.Id == id);
    }
}
