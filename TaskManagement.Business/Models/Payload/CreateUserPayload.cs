namespace TaskManagement.Business.Models.Payload
{
    public record CreateUserPayload
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string User { get; set; } = null!;
        public int RolId { get; set; }
    }
}
