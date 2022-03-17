namespace IMBox.Shared.Infrastructure.Auth
{
    public class JwtAuthSettings
    {
        public string AccessTokenSecret { get; init; }
        public double AccessTokenExpiryInMin { get; set; }
    }
}