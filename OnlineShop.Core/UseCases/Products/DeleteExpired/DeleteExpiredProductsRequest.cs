namespace OnlineShop.Core.UseCases.Products.DeleteExpired;

public class DeleteExpiredProductsRequest
{
    public DateTime CutoffDate { get; set; }
}