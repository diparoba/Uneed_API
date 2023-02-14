namespace Uneed_API.Models
{
    public class Auth
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? CurrentPassword {get; set;}
        public string? NewPassword {get; set;}
        public string? ConfirmPassword {get; set;}
    }
        
}
