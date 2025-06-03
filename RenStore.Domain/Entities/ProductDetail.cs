namespace RenStore.Domain.Entities;

public class ProductDetail
{
    public Guid Id { get; set; }
    public uint Article { get; set; }
    public string Brend { get; set; }
    // Страна производства
    public string? CountryOfManufacture { get; set; }
    // Особенности модели
    public string? ModelFeatures { get; set; } 
    // Декоративные элементы
    public string? DecorativeElements { get; set; }
    // комплектация
    public string? Equipment { get; set; }
    // Количество в упаковке
    public uint? QuantityPerPackage { get; set; }
    public string? Composition { get; set; }
    public ColorStatus? Color { get; set; }
    public TypeOfPackaging? TypeOfPackaging { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
}