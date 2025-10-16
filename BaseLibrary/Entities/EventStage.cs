using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class EventStage : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = default!;

        public int Order { get; set; }
        public DateTime? ScheduledStart { get; set; }

        public ICollection<Result> Results { get; set; } = new List<Result>();
    }

}
