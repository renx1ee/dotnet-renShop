namespace RenStore.Application.Features.ProductQuestion.Queries.GetQuestionWithAnswerById;

public class GetQuestionWithAnswerByIdVm
{
    public Guid QuestionId { get; set; }
    public Domain.Entities.Product Product { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public DateTime QuestionCreatedDate { get; set; }
    public string UserName { get; set; }
    public string QuestionMessage { get; set; }
    public AnswerVm? Answer { get; set; }
}

public class AnswerVm
{
    public Guid AnswerId { get; set; }
    public DateTime AnswerCreatedDate { get; set; }
    public string SellerName { get; set; }
    public string AnswerMessage { get; set; }
}