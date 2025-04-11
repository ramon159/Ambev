namespace Ambev.Domain.ValueObjects
{
    public record Address(string City, string Street, int Number, string ZipCode, string Latitude, string Longitude);
}
