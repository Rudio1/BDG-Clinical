namespace BGD.CLINICAL.Application.Abstractions.Identity;

public sealed record EmployeeUserAccessInfo(
    string EmailLogin,
    bool PendentePrimeiroAcesso,
    bool IsAdmin);
