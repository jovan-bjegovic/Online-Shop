namespace OnlineShop.Core.Models
{
    public record ProductStatusItem
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty; 
        public bool Enabled { get; init; }
    }
}