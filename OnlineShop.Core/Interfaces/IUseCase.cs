namespace OnlineShop.Core.Interfaces;

public interface IUseCase<TInput, TOutput>
{
    TOutput Execute(TInput input);
}