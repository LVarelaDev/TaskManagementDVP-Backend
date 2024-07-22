namespace TaskManagement.DataAccess.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
