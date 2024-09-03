using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<Category?> FindByIdAsync(Guid id);

        Task<Category?> UpdateCategoryAsync(Category updateCategory);

        Task<Category?> DeleteCategoryAsync(Guid id);
    }
}
