using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RenStore.Application.Features.ProductAnswer.Command.Create;
using RenStore.Application.Features.ProductAnswer.Command.Delete;
using RenStore.Application.Features.ProductQuestion.Command.Create;
using RenStore.Application.Features.ProductQuestion.Command.Delete;

namespace RenStore.WebApi.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class ProductQuestionController(IMapper mapper) : BaseController
{
    #region Question
    // TODO: Dto's
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/question/{productId:guid}/{applicationUserId}/{message}")]
    public async Task<IActionResult> CreateQuestion(Guid productId, string applicationUserId, string message)
    {
        var result = await Mediator.Send(
            new CreateProductQuestionCommand()
            {
                ProductId = productId,
                ApplicationUserId = applicationUserId,
                Message = message
            });

        if (result == Guid.Empty)
            return BadRequest();

        return Accepted();
    }
    
    [HttpDelete]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/question/{questionId:guid}")]
    public async Task<IActionResult> DeleteQuestion(Guid questionId)
    {
        await Mediator.Send(new DeleteProductQuestionCommand()
        {
            Id = questionId
        });

        return Accepted();
    }
    #endregion

    #region Answer
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/answer/{productQuestionId:guid}/{sellerId:int}/{message}")]
    public async Task<IActionResult> CreateAnswer(Guid productQuestionId, int sellerId, string message)
    {
        var result = await Mediator.Send(
            new CreateProductAnswerCommand()
            {
                ProductQuestionId = productQuestionId,
                SellerId = sellerId,
                Message = message
            });

        if (result == Guid.Empty)
            return BadRequest();

        return Accepted();
    }
    
    [HttpDelete]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/answer/{answerId:guid}")]
    public async Task<IActionResult> DeleteAnswer(Guid answerId)
    {
        await Mediator.Send(
            new DeleteProductAnswerCommand()
            {
                Id = answerId
            });

        return Accepted();
    }
    
    #endregion

    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/question-with-answer/{questionId:guid}")]
    public async Task<IActionResult> GetQuestionWithAnswer(Guid questionId)
    {
        return Ok();
    }
}