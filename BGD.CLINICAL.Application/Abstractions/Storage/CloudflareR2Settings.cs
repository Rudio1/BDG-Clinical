namespace BGD.CLINICAL.Application.Abstractions.Storage;

public sealed class CloudflareR2Settings
{
    public string AccountId { get; set; } = string.Empty;

    public string BucketName { get; set; } = string.Empty;

    public string AccessKeyId { get; set; } = string.Empty;

    public string SecretAccessKey { get; set; } = string.Empty;

    public string ServiceUrl { get; set; } = string.Empty;

    public string PublicBaseUrl { get; set; } = string.Empty;

    public long MaxLogoSizeBytes { get; set; } = 2 * 1024 * 1024;

    public string[] AllowedContentTypes { get; set; } =
    [
        "image/png",
        "image/jpeg",
        "image/webp",
    ];

    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(BucketName)
        && !string.IsNullOrWhiteSpace(AccessKeyId)
        && !string.IsNullOrWhiteSpace(SecretAccessKey)
        && !string.IsNullOrWhiteSpace(ServiceUrl)
        && !string.IsNullOrWhiteSpace(PublicBaseUrl);
}
