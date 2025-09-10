public record ProductPriceChangedIntegrationEvent
{
    public int ProductId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}