namespace RenStore.Application.Features.ProductQuestion.Queries.GetById;

public class GetProductQuestionByIdVm
{
    public Guid Id { get; set; }
    public Domain.Entities.Product Product { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }
}