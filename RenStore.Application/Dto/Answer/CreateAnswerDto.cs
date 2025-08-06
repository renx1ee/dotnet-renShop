namespace RenStore.Application.Dto.Answer;

public record CreateAnswerDto(Guid ProductQuestionId, int SellerId, string Message);