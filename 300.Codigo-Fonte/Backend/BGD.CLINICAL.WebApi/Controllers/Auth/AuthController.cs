using BGD.CLINICAL.Application.Identity;
using BGD.CLINICAL.Application.Identity.Abstractions;
using BGD.CLINICAL.Application.Identity.Dtos;
using BGD.CLINICAL.WebApi.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGD.CLINICAL.WebApi.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ILoginUsersService _loginUsersService;
    private readonly IRegisterCompaniesService _registerCompaniesService;
    private readonly IGetAuthenticatedUsersService _getAuthenticatedUsersService;

    public AuthController(
        ILoginUsersService loginUsersService,
        IRegisterCompaniesService registerCompaniesService,
        IGetAuthenticatedUsersService getAuthenticatedUsersService)
    {
        _loginUsersService = loginUsersService;
        _registerCompaniesService = registerCompaniesService;
        _getAuthenticatedUsersService = getAuthenticatedUsersService;
    }

    [HttpPost("registrar")]
    [AllowAnonymous]
    public async Task<IActionResult> Registrar(
        [FromBody] RegisterCompanyRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _registerCompaniesService.ExecuteAsync(request, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new ApiResponse<object?>(null!, false, result.Error));
        }

        return Ok(new ApiResponse<AuthResponse>(result.Value!, true));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _loginUsersService.ExecuteAsync(request, ip, cancellationToken);

        if (result.IsFailure)
        {
            var statusCode = result.Error == IdentityConstants.MultiplasContas
                ? StatusCodes.Status409Conflict
                : StatusCodes.Status401Unauthorized;

            return StatusCode(statusCode, new ApiResponse<object?>(null!, false, result.Error));
        }

        return Ok(new ApiResponse<AuthResponse>(result.Value!, true));
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var result = await _getAuthenticatedUsersService.ExecuteAsync(User, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(new ApiResponse<object?>(null!, false, result.Error));
        }

        return Ok(new ApiResponse<AuthenticatedUserDto>(result.Value!, true));
    }
}
