namespace OnlineShop.Core.Interfaces;

public interface IUseCase<TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest request);
}

public interface IUseCase<TResponse>
{
    Task<TResponse> Execute();
}