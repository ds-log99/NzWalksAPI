using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalksDpAPI.Data;
using NZwalksDpAPI.Models.Domain;
using System.Linq;

namespace NZwalksDpAPI.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext db;
        public SqlWalkRepository(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await db.Walks.AddAsync(walk);
            await db.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk =  await db.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }
            
            db.Walks.Remove(existingWalk);
            await db.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
                                                  string? sortBy = null, bool isAscending = true,
                                                  int pageNumber = 1, int pageSize = 1000)
        {
            var walks = db.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }

                if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);

                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
           // return await db.Walks.Include("Difficulty").Include("Region").ToListAsync();

        }

       

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await db.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await db.Walks.FindAsync(id);
            if (existingWalk == null) {
                return null;
            } 

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await db.SaveChangesAsync();

            return existingWalk;
        }
    }
}
