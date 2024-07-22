namespace TaskManagement.Business.Models.DTO
{
    public record TaskDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string User { get; set; } = null!;
        public int? AssignedUserId { get; set; }
    }
}
