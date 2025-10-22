namespace OnlineShop.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
}