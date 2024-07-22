namespace TaskManagement.Business.Models.Payload
{
    public record UpdateTaskPayload
    {
        public int code { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string User { get; set; } = null!;
        public int AssignedUserId { get; set; }
        public int Status { get; set; }
    }

    public record UpdateStatus
    {
        public int Code { get; set; }
        public int Status { get; set; }
    }
}
