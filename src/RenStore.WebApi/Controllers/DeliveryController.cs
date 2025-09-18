using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RenStore.Application.Dto.Delivery;
using RenStore.Application.Features.Delivery.Command.Delete;
using RenStore.Application.Features.Delivery.Commands.Create;
using RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesByProductId;
using RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesBySellerId;
using RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesByUserId;
using RenStore.Application.Features.Delivery.Queries.GetDeliveryById;

namespace RenStore.WebApi.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]")]
public class DeliveryController(IMapper mapper) : BaseController
{
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/delivery")]
    public async Task<IActionResult> Create(CreateDeliveryDto model)
    {
        var command = mapper.Map<CreateDeliveryCommand>(model);
        var result = await Mediator.Send(command);

        if (result == Guid.Empty)
            return BadRequest();
        
        return Created();
    }
    
    [HttpPut]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/delivery")]
    public async Task<IActionResult> Update()
    {
        return Ok();
    }

    [HttpDelete]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/delivery")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(
            new DeleteDeliveryCommand()
            {
                Id = id
            });
        
        return Accepted();
    }

    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/delivery/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(
            new GetDeliveryByIdQuery()
            {
                Id = id
            });

        if (result is not null)
        {
            return Ok(result);
        }
        
        return NotFound();
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/delivery/{productId:guid}")]
    public async Task<IActionResult> GetByProductId(Guid productId)
    {
        var result = await Mediator.Send(
            new GetAllDeliveriesByProductIdQuery()
            {
                ProductId = productId
            });

        if (result is not null)
        {
            return Ok(result);
        }
        
        return NotFound();
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/delivery/{userId¡}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var result = await Mediator.Send(
            new GetAllDeliveriesByUserIdQuery()
            {
                UserId = userId
            });

        if (result is not null)
        {
            return Ok(result);
        }
        
        return NotFound();
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/delivery/{sellerId:int}")]
    public async Task<IActionResult> GetBySellerId(int sellerId)
    {
        var result = await Mediator.Send(
            new GetAllDeliveriesBySellerIdQuery()
            {
                SellerId = sellerId
            });

        if (result is not null)
        {
            return Ok(result);
        }
        
        return NotFound();
    }
}