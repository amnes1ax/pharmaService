using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PharmaService.DataAccess.Warehouses.Exceptions;
using PharmaService.Service.Models.Warehouse;
using PharmaService.Service.Services;

namespace PharmaService.Api.Controllers;

[ApiController]
[Route("api/warehouse")]
public partial class WarehouseController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProductAsync(
        [FromServices] IWarehouseService warehouseService,
        [FromBody] [Required] CreationWarehouseModel model,
        CancellationToken cancellationToken = default)
    {
        await warehouseService.CreateAsync(new CreateWarehouseModel
        {
            PharmacyId = model.PharmacyId!.Value,
            Title = model.Title!,
        }, cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListAsync(
        [FromServices] IWarehouseService warehouseService,
        CancellationToken cancellationToken = default)
    {
        var response = await warehouseService.GetListAsync(cancellationToken);
        return Ok(response);
    }
    
    [HttpGet("{warehouseId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IWarehouseService warehouseService,
        [FromRoute] [Required] Guid warehouseId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await warehouseService.GetByIdAsync(warehouseId, cancellationToken);
            return Ok(response);
        }
        catch (WarehouseNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IWarehouseService warehouseService,
        [FromQuery] [Required] Guid warehouseId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await warehouseService.DeleteAsync(warehouseId, cancellationToken);
            return Ok();
        }
        catch (WarehouseNotFoundException)
        {
            return NotFound();
        }
    }
}