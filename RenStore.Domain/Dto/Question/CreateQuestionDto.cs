namespace RenStore.Domain.Dto.Question;

public class CreateQuestionDto
{
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public string Message { get; set; }
}