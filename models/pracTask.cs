namespace prac.Models
{
    public class pracTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Инициализация значением по умолчанию
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!; // Использование null-forgiving operator
        public bool IsActive { get; set; }
    }
}
