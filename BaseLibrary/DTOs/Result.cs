namespace BaseLibrary.DTOs
{
    public class ResultCreateDto
    {
        public int EventStageId { get; set; }
        public int AthleteId { get; set; }
        public string Name { get; set; } = string.Empty; // BaseEntity requires Name
        public double? Score { get; set; }
        public string? Unit { get; set; }
        public int? Rank { get; set; }
        public string? Note { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class ResultUpdateDto
    {
        public int Id { get; set; }
        public int EventStageId { get; set; }
        public int AthleteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double? Score { get; set; }
        public string? Unit { get; set; }
        public int? Rank { get; set; }
        public string? Note { get; set; }
        public string? ModifiedBy { get; set; }
    }

    public class ResultResponseDto
    {
        public int Id { get; set; }
        public int EventStageId { get; set; }
        public int AthleteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double? Score { get; set; }
        public string? Unit { get; set; }
        public int? Rank { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public class PublishResultsDto
    {
        public int StageId { get; set; }
    }
}