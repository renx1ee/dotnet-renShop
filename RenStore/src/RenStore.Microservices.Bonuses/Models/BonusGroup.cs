namespace RenStore.Microservices.Bonuses.Models;

public class BonusGroup
{
    public Guid Id { get; set; }
    public IEnumerable<User> Users { get; set; }
}