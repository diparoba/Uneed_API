namespace Uneed_API.DTO
{
    public class AddressUserResponse
    {
        public int AddressId { get; set; }
        public string? AddressName { get; set; }
        public string? PrincipalStreet { get; set; }
        public string? SecondaryStreet { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Lastname { get; set; }
    }
}