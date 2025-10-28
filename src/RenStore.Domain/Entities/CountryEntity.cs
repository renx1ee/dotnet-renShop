namespace RenStore.Domain.Entities;

public class CountryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public IList<CityEntity> Cities { get; set; } = [];
}