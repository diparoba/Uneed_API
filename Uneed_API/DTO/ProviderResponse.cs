namespace Uneed_API.DTO
{
    public class ProviderResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserLastname { get; set; }
        public string? UserEmail{get; set;}
        public string? Status { get; set; }
        public string? Identification { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? ServName {get; set;}
        public string? Description {get; set;}
        public int CategoryId{get; set;}
        public int UserId{get; set;}
        public string? CategoryName {get; set;}

    }
}