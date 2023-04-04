namespace LMRestApi2023.Models
{
    public class LoggedUser
    {

        public string Username { get; set; } = string.Empty;
        public int AccesslevelId { get; set; }
        public string? Token { get; set; }

    }
}
