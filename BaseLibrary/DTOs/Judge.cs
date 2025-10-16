namespace BaseLibrary.DTOs
{
    public class AssignJudgeToCompetitionDto
    {
        public int CompetitionId { get; set; }
        public int JudgeId { get; set; }
    }

    public class AssignJudgeToEventDto
    {
        public int EventId { get; set; }
        public int JudgeId { get; set; }
    }
}