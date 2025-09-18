namespace RenStore.Domain.Entities.Products;

public class ShoesProduct
{
    public Guid Id { get; set; }
    public FullnessOfShoes? FullnessOfShoes { get; set; }
    public ShoeInsoleMaterial? ShoeInsoleMaterial { get; set; }
    public ShoeLiningMaterial? ShoeLiningMaterial { get; set; }
    public ShoeSoleMaterial? ShoeSoleMaterial { get; set; }
    public SoleFasteningMethod? SoleFasteningMethod { get; set; }
    public TypeOfFastener? TypeOfFastener { get; set; }
    // Высота подошвы
    public uint? SoleHeight { get; set; }
    public Gender? Gender { get; set; }
    public Season? Season { get; set; }
    // Забота о вещах
    public string? TakingCareOfThings { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
}