using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace RenStore.WebApi.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ShippingController : BaseController
{
    //TODO:
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/methods")]
    public async Task<ActionResult> Methods()
    {
        return Ok();
    }
    
    //TODO:
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/calculate")]
    public async Task<ActionResult> Calculate()
    {
        return Ok();
    }
    
    //TODO:
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/track")]
    public async Task<ActionResult> Track()
    {
        return Ok();
    }
}