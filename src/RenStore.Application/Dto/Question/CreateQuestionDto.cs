namespace RenStore.Application.Dto.Question;

public record CreateQuestionDto(Guid ProductId, string ApplicationUserId, string Message);