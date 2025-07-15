namespace RenStore.Domain.Dto.Question;

public record CreateQuestionDto(Guid ProductId, string ApplicationUserId, string Message);