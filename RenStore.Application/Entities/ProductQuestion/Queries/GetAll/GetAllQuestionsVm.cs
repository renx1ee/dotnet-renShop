namespace RenStore.Application.Entities.ProductQuestion.Queries.GetAll;

public class GetAllQuestionsVm
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }

    public GetAllQuestionsVm(Guid id, Guid productId, string applicationUserId, 
        DateTime createdDate, string userName, string message)
    {
        Id = id;
        ProductId = productId;
        ApplicationUserId = applicationUserId;
        CreatedDate = createdDate;
        UserName = userName;
        Message = message;
    }   
}