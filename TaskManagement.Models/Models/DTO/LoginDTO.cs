namespace TaskManagement.Models.Models.DTO
{
    public record LoginDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
