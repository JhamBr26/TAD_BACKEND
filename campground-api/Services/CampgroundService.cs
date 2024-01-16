using campground_api.Models;
using campground_api.Models.Dto;
using campground_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace campground_api.Services
{
    public class CampgroundService
    {
        private readonly CampgroundContext _context;
        public CampgroundService(CampgroundContext context)
        {
            _context = context;
        }

        public async Task<List<CampgroundListDto>> GetAll()
        {
            var campgrounds = await _context.Campgrounds
                .Include(c => c.Images)
                .Include(c => c.Reviews)
                .ToListAsync();

            // mapear a lista de campgrounds para uma lista de CampgroundListDto
            return campgrounds.Select(Mapper.MapCampgroundToCampgroundListDto).ToList();
        }

        public async Task<CampgroundGetDto?> Get(int id)
        {
            var campground = await _context.Campgrounds
                .Include(c => c.Images)
                .Include(c => c.Reviews)
                .ThenInclude(r => r.User)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(campground == null) return null;

            return Mapper.MapCampgroundToCampgroundGetDto(campground);
        }
           
        public async Task<CampgroundGetDto?> Create(int userId, CampgroundCreateDto campgroundDto)
        {
            Campground? newCampground = null;
            try
            {
                newCampground = new Campground()
                {
                    UserId = userId,
                    Title = campgroundDto.Title,
                    Latitude = campgroundDto.Latitude,
                    Longitude = campgroundDto.Longitude,
                    Price = campgroundDto.Price,
                    Description = campgroundDto.Description,
                    Location = campgroundDto.Location
                };

                _context.Campgrounds.Add(newCampground);

                await _context.SaveChangesAsync();

                campgroundDto.Images?.ForEach(image =>
                {
                    _context.Images.Add(new Image()
                    {
                        CampgroundId = newCampground.Id,
                        Url = image.Url,
                        Filename = image.Filename,
                    });
                });

                await _context.SaveChangesAsync();

                return await this.Get(newCampground.Id);
            }
            catch(Exception)
            {
                throw;
            }

        }

        public async Task<Campground?> Delete(int id)
        {
            var campground = await _context.Campgrounds.FindAsync(id);
            if(campground == null)
            {
                return null;
            }

            _context.Campgrounds.Remove(campground);
            await _context.SaveChangesAsync();

            return campground;
        }

        public async Task<CampgroundGetDto?> Update(int id, CampgroundCreateDto campgroundDto)
        {
            var campground = await _context.Campgrounds.FindAsync(id);
            if(campground == null) return null;

            campground.UserId = campground.UserId;
            campground.Title = campgroundDto.Title;
            campground.Latitude = campgroundDto.Latitude;
            campground.Longitude = campgroundDto.Longitude;
            campground.Price = campgroundDto.Price;
            campground.Description = campgroundDto.Description;
            campground.Location = campgroundDto.Location;

            await _context.SaveChangesAsync();

            var imagesToDelete = await _context.Images.Where(image => image.CampgroundId == campground.Id).ToListAsync();
            _context.Images.RemoveRange(imagesToDelete);

            campgroundDto.Images?.ForEach(image =>
            {
                _context.Images.Add(new Image()
                {
                    CampgroundId = campground.Id,
                    Url = image.Url,
                    Filename = image.Filename,
                });
            });

            await _context.SaveChangesAsync();
            return await this.Get(campground.Id);
        }

    }
}
