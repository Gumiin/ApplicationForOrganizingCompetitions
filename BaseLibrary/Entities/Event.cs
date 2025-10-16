using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Event : BaseEntity
    {
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; } = default!;

        public string Category { get; set; } = string.Empty;        // np. "Mężczyźni", "Kobiety"
        public string? Description { get; set; }

        public ICollection<EventStage> Stages { get; set; } = new List<EventStage>();
        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
        public ICollection<Judge> Judges { get; set; } = new List<Judge>();
    }

}
