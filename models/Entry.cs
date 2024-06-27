namespace prac.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public string Description { get; set; } = string.Empty; // Инициализация значением по умолчанию
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!; // Использование null-forgiving operator
    }
}
