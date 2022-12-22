using AutoPartsV1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoPartsV1.Data.Repositories
{
    public interface ICarPartsRepository
    {
        Task<CarPart?> GetAsync(int carId, int carPartId);
        Task<CarPart?> GetCarPartAsync(int carPartId);
        Task<IReadOnlyList<CarPart>> GetManyAsync(int carId);
        Task<IReadOnlyList<CarPart>> GetManyAsync();
        Task CreateAsync(CarPart carPart);
        Task UpdateAsync(CarPart carPart);
        Task DeleteAsync(CarPart carPart);
    }

    public class CarPartsRepository : ICarPartsRepository
    {
        private readonly ForumDbContext _forumDbContext;

        public CarPartsRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task<CarPart?> GetCarPartAsync(int carPartId)
        {
            return await _forumDbContext.CarParts.FirstOrDefaultAsync(x => x.Id == carPartId);
        }

        public async Task<CarPart?> GetAsync(int carId, int carPartId)
        {
            return await _forumDbContext.CarParts.FirstOrDefaultAsync(x => x.Id == carPartId && x.CarId == carId);
        }

        public async Task<IReadOnlyList<CarPart>> GetManyAsync(int carId)
        {
            return await _forumDbContext.CarParts.Where(x => x.CarId == carId).ToListAsync();
        }

        public async Task<IReadOnlyList<CarPart>> GetManyAsync()
        {
            return await _forumDbContext.CarParts.Where(x => x.CarId != 0).ToListAsync();
        }

        public async Task CreateAsync(CarPart carPart)
        {
            _forumDbContext.CarParts.Add(carPart);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CarPart carPart)
        {
            _forumDbContext.CarParts.Remove(carPart);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CarPart carPart)
        {
            _forumDbContext.CarParts.Update(carPart);
            await _forumDbContext.SaveChangesAsync();
        }
    }
}
