namespace OnlineShop.Core.Interfaces;

public interface IUseCase<TRequest, TResponse>
{
    TResponse Execute(TRequest request);
}

public interface IUseCase<TResponse>
{
    TResponse Execute();
}