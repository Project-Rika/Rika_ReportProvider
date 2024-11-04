namespace Rika_ReportProvider.Models;

public class Product
{
    public string? Id { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductSalePrice { get; set; }
    public string? ProductDescription { get; set; }
    public int ProductCategoryId { get; set; } 
    public string? ProductSize { get; set; }
    public string? ProductColor { get; set; }
}