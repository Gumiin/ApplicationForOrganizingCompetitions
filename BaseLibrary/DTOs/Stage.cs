namespace BaseLibrary.DTOs
{
    public class StageCreateDto
    {
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public DateTime? ScheduledStart { get; set; }
    }

    public class StageUpdateDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public DateTime? ScheduledStart { get; set; }
    }

    public class StageResponseDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public DateTime? ScheduledStart { get; set; }
    }
}