using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Judge : BaseEntity
    {
        public string? Email { get; set; }
        public string? Role { get; set; }                           // np. "Sędzia główny", "Asystent"
        public string? UserId { get; set; }                         // jeśli powiązany z kontem użytkownika

        public int? CompetitionId { get; set; }
        public Competition? Competition { get; set; }

        public int? EventId { get; set; }
        public Event? Event { get; set; }
    }

}
