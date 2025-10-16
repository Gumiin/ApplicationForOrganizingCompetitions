using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Athlete : BaseEntity
    {
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Club { get; set; }

        public string? UserId { get; set; }                         // jeśli połączony z kontem użytkownika

        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
        public ICollection<Result> Results { get; set; } = new List<Result>();
    }

}
