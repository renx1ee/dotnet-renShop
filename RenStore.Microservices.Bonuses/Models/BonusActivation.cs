namespace RenStore.Microservices.Bonuses.Models;

public class BonusActivation
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Bonus Bonus { get; set; }
    public Guid BonusId { get; set; }
}