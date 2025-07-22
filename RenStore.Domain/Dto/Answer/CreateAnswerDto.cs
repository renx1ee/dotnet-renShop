namespace RenStore.Domain.Dto.Answer;

public record CreateAnswerDto(Guid ProductQuestionId, int SellerId, string Message);