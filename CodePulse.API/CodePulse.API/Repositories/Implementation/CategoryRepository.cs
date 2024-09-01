using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Category> CreateAsync(Category newCategory)
        {
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category?> FindByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> UpdateCategoryAsync(Category updateCategory)
        {
           var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == updateCategory.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = updateCategory.Name;
                existingCategory.UrlHandle = updateCategory.UrlHandle;
                await _context.SaveChangesAsync();
                return existingCategory;
            }
            return null;
        }
    }
}
