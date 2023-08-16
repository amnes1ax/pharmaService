using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PharmaService.DataAccess.Pharmacies.Exceptions;
using PharmaService.Service.Models.Pharmacy;
using PharmaService.Service.Services;

namespace PharmaService.Api.Controllers;

[ApiController]
[Route("api/pharmacy")]
public partial class PharmacyController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePharmacyAsync(
        [FromServices] IPharmacyService pharmacyService,
        [FromBody] [Required] CreationPharmacyModel model,
        CancellationToken cancellationToken = default)
    {
        await pharmacyService.CreateAsync(new CreatePharmacyModel
        {
            Title = model.Title!,
            Address = model.Address,
            PhoneNumber = model.PhoneNumber
        }, cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListAsync(
        [FromServices] IPharmacyService pharmacyService,
        CancellationToken cancellationToken = default)
    {
        var response = await pharmacyService.GetListAsync(cancellationToken);
        return Ok(response);
    }
    
    [HttpGet("{pharmacyId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IPharmacyService pharmacyService,
        [FromRoute] [Required] Guid pharmacyId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await pharmacyService.GetByIdAsync(pharmacyId, cancellationToken);
            return Ok(response);
        }
        catch (PharmacyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IPharmacyService pharmacyService,
        [FromQuery] [Required] Guid pharmacyId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await pharmacyService.DeleteAsync(pharmacyId, cancellationToken);
            return Ok();
        }
        catch (PharmacyNotFoundException)
        {
            return NotFound();
        }
    }
}