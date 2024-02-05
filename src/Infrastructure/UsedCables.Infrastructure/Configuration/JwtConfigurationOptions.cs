namespace UsedCables.Infrastructure.Configuration
{
    public class JwtConfigurationOptions
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}