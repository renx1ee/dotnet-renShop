using RenStore.Microservices.Bonuses.Enums;

namespace RenStore.Microservices.Bonuses.Models;

public class Bonus
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartedDate { get; set; }
    public DateTime FinishedDate { get; set; }
    public uint MaxActivations { get; set; }
    public uint ActivatedCount { get; set; }
    public bool IsActive { get; set; }
    public BonusType Type { get; set; }
}