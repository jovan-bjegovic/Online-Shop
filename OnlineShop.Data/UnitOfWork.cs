using OnlineShop.Core.Interfaces;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public ICategoryRepository Categories { get; } = new DbCategoryRepository(context);

    public async Task<int> CommitAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}