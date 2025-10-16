using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Result : BaseEntity
    {
        public int EventStageId { get; set; }
        public EventStage EventStage { get; set; } = default!;

        public int AthleteId { get; set; }
        public Athlete Athlete { get; set; } = default!;

        public double? Score { get; set; }                          // np. 10.52
        public string? Unit { get; set; }                           // np. "s", "m", "pkt"
        public int? Rank { get; set; }
        public string? Note { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }

}
