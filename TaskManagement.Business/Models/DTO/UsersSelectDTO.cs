namespace TaskManagement.Business.Models.DTO
{
    public record UsersSelectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
