﻿namespace RenStore.Application.Features.ProductQuestion.Queries.GetAllByUserId;

public class GetAllQuestionsByUserIdVm
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }

    public GetAllQuestionsByUserIdVm(Guid id, Guid productId, string applicationUserId, 
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