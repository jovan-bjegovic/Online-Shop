namespace OnlineShop.Core.UseCases.Categories.DeleteExpired;

public class DeleteExpiredCategoriesRequest
{
    public DateTime CutoffDate { get; set; }
}