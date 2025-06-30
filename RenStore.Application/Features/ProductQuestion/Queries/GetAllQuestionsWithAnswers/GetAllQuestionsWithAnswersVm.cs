namespace RenStore.Application.Features.ProductQuestion.Queries.GetAllQuestionsWithAnswers;

public class GetAllQuestionsWithAnswersVm
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }
    
    public GetAllQuestionsWithAnswersAnswerVm Answer { get; set; }

    public GetAllQuestionsWithAnswersVm(Guid id, Guid productId, string applicationUserId, 
        DateTime createdDate, string userName, string message, GetAllQuestionsWithAnswersAnswerVm answer)
    {
        Id = id;
        ProductId = productId;
        ApplicationUserId = applicationUserId;
        CreatedDate = createdDate;
        UserName = userName;
        Message = message;
        Answer = answer;
    }   
}

public class GetAllQuestionsWithAnswersAnswerVm
{
    public Guid AnswerId { get; set; }
    public DateTime AnswerCreatedDate { get; set; }
    public string SellerName { get; set; }
    public string AnswerMessage { get; set; }
}