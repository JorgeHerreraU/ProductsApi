namespace ProductsApi.Security;

public class AppIdentitySettings
{
    public string Audience { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Key { get; set; } = null!;
}
