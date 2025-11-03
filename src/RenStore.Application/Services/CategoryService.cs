/*
using RenStore.Application.Repository;

namespace RenStore.Application.Services.CategoryEntity;

public class CategoryService
{
    private readonly ICategoryRepository categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository) =>
        this.categoryRepository = categoryRepository;
    
    public Task<IEnumerable<Domain.Entities.CategoryEntity>> GetAllCategoryAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.CategoryEntity> GetCategoryByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Domain.Entities.CategoryEntity> GetByName(string name)
    {
        var category = await categoryRepository.GetByNameAsync(name);
        
        if (category != null)
            return category;
        
        return null;
    }

    public async Task<IEnumerable<Domain.Entities.CategoryEntity>> GetAllCategory(CancellationToken cancellationToken)
    {
        return await categoryRepository.GetAllAsync(cancellationToken);
    }
}
*/
