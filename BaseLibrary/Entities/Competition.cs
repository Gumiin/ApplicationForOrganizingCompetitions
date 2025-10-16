using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Competition : BaseEntity
    {
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public bool IsPublished { get; set; } = false;

        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Judge> Judges { get; set; } = new List<Judge>();
    }

}
