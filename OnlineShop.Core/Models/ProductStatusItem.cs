namespace OnlineShop.Core.Models
{
    public record ProductStatusItem
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty; 
        public bool Enabled { get; init; }
    }
}