using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PharmaService.DataAccess.Products.Exceptions;
using PharmaService.Service.Models.Product;
using PharmaService.Service.Services;

namespace PharmaService.Api.Controllers;

[ApiController]
[Route("api/product")]
public partial class ProductController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProductAsync(
        [FromServices] IProductService productService,
        [FromBody] [Required] CreationProductModel model,
        CancellationToken cancellationToken = default)
    {
        await productService.CreateAsync(new CreateProductModel
        {
            Title = model.Title!,
            ShelfLife = model.ShelfLife
        }, cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListAsync(
        [FromServices] IProductService productService,
        CancellationToken cancellationToken = default)
    {
        var response = await productService.GetListAsync(cancellationToken);
        return Ok(response);
    }
    
    [HttpGet("by-pharmacy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListByPharmacyAsync(
        [FromServices] IProductService productService,
        [FromQuery] Guid pharmacyId,
        CancellationToken cancellationToken = default)
    {
        var response = await productService.GetListByPharmacyAsync(pharmacyId, cancellationToken);
        return Ok(response);
    }
    
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IProductService productService,
        [FromRoute] [Required] Guid productId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await productService.GetByIdAsync(productId, cancellationToken);
            return Ok(response);
        }
        catch (ProductNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IProductService productService,
        [FromQuery] [Required] Guid productId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await productService.DeleteAsync(productId, cancellationToken);
            return Ok();
        }
        catch (ProductNotFoundException)
        {
            return NotFound();
        }
    }
}