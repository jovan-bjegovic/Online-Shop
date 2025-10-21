namespace OnlineShop.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository Categories { get; }
    Task<int> CommitAsync();
}