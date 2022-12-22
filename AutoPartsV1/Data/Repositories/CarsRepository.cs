using AutoPartsV1.Data.Entities;
using AutoPartsV1.Data.Dtos.Cars;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsV1.Data.Repositories
{
    public interface ICarsRepository
    {
        Task<Car?> GetByBrandAsync(int carId, int brandId);
        Task<Car?> GetAsync(int carId);
        Task<IReadOnlyList<Car>> GetManyByBrandAsync(int brandId);
        Task<IReadOnlyList<Car>> GetManyAsync();
        Task CreateAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(Car car);
    }

    public class CarsRepository : ICarsRepository
    {
        private readonly ForumDbContext _forumDbContext;

        public CarsRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task<Car?> GetByBrandAsync(int carId, int brandId)
        {
            return await _forumDbContext.Cars.FirstOrDefaultAsync(x => x.Id == carId && x.BrandId == brandId);
        }

        public async Task<Car?> GetAsync(int carId)
        {
            return await _forumDbContext.Cars.FirstOrDefaultAsync(x => x.Id == carId);
        }

        public async Task<IReadOnlyList<Car>> GetManyByBrandAsync(int brandId)
        {
            return await _forumDbContext.Cars.Where(x => x.BrandId == brandId).ToListAsync();
        }

        public async Task<IReadOnlyList<Car>> GetManyAsync()
        {
            return await _forumDbContext.Cars.Where(x => x.Id != 0).ToListAsync();
        }

        public async Task CreateAsync(Car car)
        {
            _forumDbContext.Cars.Add(car);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Car car)
        {
            _forumDbContext.Cars.Remove(car);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car car)
        {
            _forumDbContext.Cars.Update(car);
            await _forumDbContext.SaveChangesAsync();
        }

    }
}
