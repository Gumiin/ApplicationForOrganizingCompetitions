namespace BaseLibrary.DTOs
{
    public class EventCreateDto
    {
        public int CompetitionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class EventUpdateDto
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class EventResponseDto
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}