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
        public int OrganizerId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public int MaxCompetitors { get; set; } = 1;
        public int RoundsAmount { get; set; } = 1;
        public string? Description { get; set; }
        public string? Localization { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPerson { get; set; }
        public int WinnerID { get; set; } = 0;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
