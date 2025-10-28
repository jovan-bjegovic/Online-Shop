using OnlineShop.Core.Interfaces;

namespace OnlineShop.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> CommitAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}