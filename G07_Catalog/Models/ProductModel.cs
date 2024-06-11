namespace G07_Catalog.Models
{
    public record ProductModel(
        int Id,
        string Name,
        string CategoryName,
        string? QuantityPerUnit,
        decimal? UnitPrice,
        short? UnitsInStock,
        short? UnitsOnOrder,
        short? ReorderLevel,
        bool? Discontinued
    );
}
