namespace CBT.Infrastructure.Common.Options
{
    public class JWTOptions
    {
        public static string SectionName = nameof(JWTOptions);
        public static string CookiesName = "Auth";
        public static string RefreshCookiesName = "Refresh";
        public required string SecretKey { get; set; }
        public string? Issuer = null;
        public string? Audience = null;
        public TimeSpan ExpireTime { get; set; } = TimeSpan.Zero;
        public TimeSpan RefreshExpireTime { get; set; } = TimeSpan.Zero;
    }
}
