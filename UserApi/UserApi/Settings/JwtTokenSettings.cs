namespace UserApi.Settings
{
    public class JwtTokenSettings
    {
        public string JwtIssuer { get; set; }

        public string JwtAudience { get; set; }

        public string JwtSecretKey { get; set; }
    }
}
