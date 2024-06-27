namespace prac.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Инициализация значением по умолчанию
        public string Code { get; set; } = string.Empty; // Инициализация значением по умолчанию
        public bool IsActive { get; set; }
        
        // Связанные задачи
        public ICollection<Task> Tasks { get; set; } = new List<Task>(); // Инициализация коллекции
    }
}
