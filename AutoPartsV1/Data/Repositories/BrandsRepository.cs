using AutoPartsV1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoPartsV1.Data.Repositories
{
    public interface IBrandsRepository
    {
        Task<Brand?> GetAsync(int brandId);
        Task<IReadOnlyList<Brand>> GetManyAsync();
        Task CreateAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task DeleteAsync(Brand brand);
    }

    public class BrandsRepository : IBrandsRepository
    {
        private readonly ForumDbContext _forumDbContext;

        public BrandsRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task<Brand?> GetAsync(int brandId)
        {
            return await _forumDbContext.Brands.FirstOrDefaultAsync(o => o.Id == brandId);
        }

        public async Task<IReadOnlyList<Brand>> GetManyAsync()
        {
            return await _forumDbContext.Brands.ToListAsync();
        }

        public async Task CreateAsync(Brand brand)
        {
            _forumDbContext.Brands.Add(brand);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Brand brand)
        {
            _forumDbContext.Brands.Update(brand);
            await _forumDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Brand brand)
        {
            _forumDbContext.Brands.Remove(brand);
            await _forumDbContext.SaveChangesAsync();
        }

    }
}
