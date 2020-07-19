namespace aspnetcore_starter.Config
{
    public class AuthenticationSettings
    {
        public string Authority { get; set; }
        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int CacheInMinutes { get; set; }
    }
}