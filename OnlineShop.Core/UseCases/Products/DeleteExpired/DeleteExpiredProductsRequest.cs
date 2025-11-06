namespace OnlineShop.Core.UseCases.Products.DeleteExpired;

public abstract class DeleteExpiredProductsRequest
{
    public DateTime CutoffDate { get; set; }
}