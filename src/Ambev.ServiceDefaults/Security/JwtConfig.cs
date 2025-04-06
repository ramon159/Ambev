namespace Ambev.ServiceDefaults.Security
{
    sealed record JwtConfig(string SecretKey, int TokenDuration);
}
