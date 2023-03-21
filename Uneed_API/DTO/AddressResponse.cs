namespace Uneed_API.DTO
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string? Name { get; set;}
        public string? PrincipalStreet { get; set; }
        public string? SecondaryStreet { get; set; }
        public string? City { get; set; }
    }
}