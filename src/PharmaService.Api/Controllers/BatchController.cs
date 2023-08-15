using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PharmaService.Service.Models.Batch;
using PharmaService.Service.Services;

namespace PharmaService.Api.Controllers;

[ApiController]
[Route("api/batch")]
public partial class BatchController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBatchAsync(
        [FromServices] IBatchService batchService,
        [FromBody] [Required] CreationBatchModel model,
        CancellationToken cancellationToken = default)
    {
        await batchService.CreateAsync(new CreateBatchModel
        {
            ProductId = model.ProductId!.Value,
            ProductCount = model.ProductCount!.Value,
            CreatedOn = model.CreatedOn,
            WarehouseId = model.WarehouseId!.Value
        }, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListAsync(
        [FromServices] IBatchService batchService,
        CancellationToken cancellationToken = default)
    {
        var response = await batchService.GetListAsync(cancellationToken);
        return Ok(response);
    }
    
    [HttpGet("{batchId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListAsync(
        [FromServices] IBatchService batchService,
        [FromRoute] [Required] Guid batchId,
        CancellationToken cancellationToken = default)
    {
        var response = await batchService.GetByIdAsync(batchId, cancellationToken);
        return Ok(response);
    }
    
    [HttpGet("by-warehouse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListByWarehouseAsync(
        [FromServices] IBatchService batchService,
        [FromQuery] Guid warehouseId,
        CancellationToken cancellationToken = default)
    {
        var response = await batchService.GetListByWarehouseAsync(warehouseId, cancellationToken);
        return Ok(response);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IBatchService batchService,
        [FromQuery] [Required] Guid batchId,
        CancellationToken cancellationToken = default)
    {
        await batchService.DeleteAsync(batchId, cancellationToken);
        return Ok();
    }
}