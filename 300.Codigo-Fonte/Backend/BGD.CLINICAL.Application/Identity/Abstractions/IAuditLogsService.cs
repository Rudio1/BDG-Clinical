namespace BGD.CLINICAL.Application.Identity.Abstractions;

public interface IAuditLogsService
{
    Task RegisterLoginAsync(
        Guid usuarioId,
        Guid empresaId,
        string? ip,
        CancellationToken cancellationToken = default);
}
