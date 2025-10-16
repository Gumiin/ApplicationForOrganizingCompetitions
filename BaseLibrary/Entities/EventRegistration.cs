using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class EventRegistration : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = default!;

        public int AthleteId { get; set; }
        public Athlete Athlete { get; set; } = default!;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
    }

}
