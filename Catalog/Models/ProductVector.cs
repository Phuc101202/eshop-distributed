using Microsoft.Extensions.VectorData;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Models;

public class ProductVector
{
    [VectorStoreKey]
    public int Id { get; set; }
    [VectorStoreData]
    public string Name { get; set; } = default!;
    [VectorStoreData]
    public string Description { get; set; } = default!;
    [VectorStoreData]
    public decimal Price { get; set; }
    [VectorStoreData]
    public string ImageUrl { get; set; } = default!;

    [NotMapped]
    [VectorStoreVector(384)]
    public ReadOnlyMemory<float> Vector { get; set; }
}