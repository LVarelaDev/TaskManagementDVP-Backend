namespace TaskManagement.Business.Models.Responses
{
    public record TaskResponse
    {
        public int Code { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int StatusId { get; set; }
        public bool Active { get; set; }
        public int? AssignedUserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; } = null!;
    }
}
