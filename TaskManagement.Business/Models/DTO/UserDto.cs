namespace TaskManagement.Models.Models.DTO
{
    public record UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RolId { get; set; }
    }
}
