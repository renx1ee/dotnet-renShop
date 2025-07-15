using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RenStore.Application.Features.ProductAnswer.Command.Create;
using RenStore.Application.Features.ProductAnswer.Command.Delete;
using RenStore.Application.Features.ProductQuestion.Command.Create;
using RenStore.Application.Features.ProductQuestion.Command.Delete;
using RenStore.Application.Features.ProductQuestion.Queries.GetAll;
using RenStore.Application.Features.ProductQuestion.Queries.GetAllByProductId;
using RenStore.Application.Features.ProductQuestion.Queries.GetAllByUserId;
using RenStore.Application.Features.ProductQuestion.Queries.GetQuestionWithAnswerById;
using RenStore.Domain.Dto.Question;

namespace RenStore.WebApi.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class ProductQuestionController(IMapper mapper) : BaseController
{
    #region Question
    // {productId:guid}/{applicationUserId}/{message}
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/question/")]
    public async Task<IActionResult> CreateQuestion(CreateQuestionDto dto)
    {
        var result = await Mediator.Send(
            new CreateProductQuestionCommand()
            {
                ProductId = dto.ProductId,
                ApplicationUserId = dto.ApplicationUserId,
                Message = dto.Message
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
        await Mediator.Send(
            new DeleteProductQuestionCommand()
            {
                Id = questionId
            });
        
        return Accepted();
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/questions/")]
    public async Task<IActionResult> GetAllQuestions()
    {
        var result = await Mediator.Send(
            new GetAllQuestionsQuery());

        if (result.Any()) 
            return Ok(result); 
        
        return NotFound(); 
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/question/{productId:guid}")]
    public async Task<IActionResult> GetAllQuestionsByProductId(Guid productId)
    {
        var result = await Mediator.Send(
            new GetAllQuestionsByProductIdQuery()
            {
                ProductId = productId
            });

        if (result.Any()) 
            return Ok(result); 
        
        return NotFound(); 
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/question/{userId}")]
    public async Task<IActionResult> GetAllQuestionsByUserId(string userId)
    {
        var result = await Mediator.Send(
            new GetAllQuestionsByUserIdQuery()
            {
                UserId = userId
            });

        if (result.Any()) 
            return Ok(result); 
        
        return NotFound(); 
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/question-with-answer/{questionId:guid}")]
    public async Task<IActionResult> GetQuestionWithAnswer(Guid questionId)
    {
        var result = await Mediator.Send(
            new GetQuestionWithAnswerByIdQuery()
            {
                Id = questionId
            });

        if (result is null) 
            return Ok(result);
        
        return NotFound();
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
}