using BGD.CLINICAL.Application.Core.Dtos;
using BGD.CLINICAL.Application.Core.Employees;
using BGD.CLINICAL.Application.Identity;
using BGD.CLINICAL.WebApi.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGD.CLINICAL.WebApi.Controllers.Employee;

[ApiController]
[Authorize]
[Route("api/employees")]
public sealed class EmployeeController : ControllerBase
{
    private readonly ICreateEmployeesService _createEmployeesService;
    private readonly IListEmployeesService _listEmployeesService;
    private readonly IGetEmployeesService _getEmployeesService;
    private readonly IUpdateEmployeesService _updateEmployeesService;
    private readonly IDeactivateEmployeesService _deactivateEmployeesService;
    private readonly IReactivateEmployeesService _reactivateEmployeesService;

    public EmployeeController(
        ICreateEmployeesService createEmployeesService,
        IListEmployeesService listEmployeesService,
        IGetEmployeesService getEmployeesService,
        IUpdateEmployeesService updateEmployeesService,
        IDeactivateEmployeesService deactivateEmployeesService,
        IReactivateEmployeesService reactivateEmployeesService)
    {
        _createEmployeesService = createEmployeesService;
        _listEmployeesService = listEmployeesService;
        _getEmployeesService = getEmployeesService;
        _updateEmployeesService = updateEmployeesService;
        _deactivateEmployeesService = deactivateEmployeesService;
        _reactivateEmployeesService = reactivateEmployeesService;
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] Guid? unidadeId = null,
        [FromQuery] bool includeInactive = false,
        CancellationToken cancellationToken = default)
    {
        var result = await _listEmployeesService.ExecuteAsync(unidadeId, includeInactive, cancellationToken);

        return Ok(new ApiResponse<IReadOnlyList<EmployeeDto>>(result.Value!, true));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _getEmployeesService.ExecuteAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new ApiResponse<object?>(null!, false, result.Error));
        }

        return Ok(new ApiResponse<EmployeeDto>(result.Value!, true));
    }

    [HttpPost]
    [Authorize(Policy = IdentityConstants.PolicyAdmin)]
    public async Task<IActionResult> Create(
        [FromBody] CreateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _createEmployeesService.ExecuteAsync(request, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new ApiResponse<object?>(null!, false, result.Error));
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Value!.Id },
            new ApiResponse<EmployeeDto>(result.Value, true));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = IdentityConstants.PolicyAdmin)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _updateEmployeesService.ExecuteAsync(id, request, cancellationToken);

        if (result.IsFailure)
        {
            var statusCode = result.Error == "Funcionário não encontrado."
                ? StatusCodes.Status404NotFound
                : StatusCodes.Status400BadRequest;

            return StatusCode(statusCode, new ApiResponse<object?>(null!, false, result.Error));
        }

        return Ok(new ApiResponse<EmployeeDto>(result.Value!, true));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _deactivateEmployeesService.ExecuteAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            var statusCode = result.Error == "Funcionário não encontrado."
                ? StatusCodes.Status404NotFound
                : StatusCodes.Status400BadRequest;

            return StatusCode(statusCode, new ApiResponse<object?>(null!, false, result.Error));
        }

        return Ok(new ApiResponse<EmployeeDto>(result.Value!, true));
    }

    [HttpPatch("{id:guid}/reactivate")]
    public async Task<IActionResult> Reactivate(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reactivateEmployeesService.ExecuteAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            var statusCode = result.Error == "Funcionário não encontrado."
                ? StatusCodes.Status404NotFound
                : StatusCodes.Status400BadRequest;

            return StatusCode(statusCode, new ApiResponse<object?>(null!, false, result.Error));
        }

        return Ok(new ApiResponse<EmployeeDto>(result.Value!, true));
    }
}
