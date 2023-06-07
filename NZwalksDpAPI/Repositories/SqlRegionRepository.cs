using Microsoft.EntityFrameworkCore;
using NZwalksDpAPI.Data;
using NZwalksDpAPI.Models.Domain;
using NZwalksDpAPI.Models.DTO;

namespace NZwalksDpAPI.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext db;
        public SqlRegionRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public async Task<Region> CreateAsync(Region region)
        {
           await db.Regions.AddAsync(region);
           await db.SaveChangesAsync();
           return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await db.Regions.FindAsync(id);
            if (existingRegion == null) 
            {
                return null;
            }

            db.Regions.Remove(existingRegion);
            await db.SaveChangesAsync();
            return existingRegion;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await db.Regions.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await db.Regions.FindAsync(id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await db.Regions.FindAsync(id);
            if (existingRegion == null)
            { 
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await db.SaveChangesAsync();

            return existingRegion;
        }
    }
}
