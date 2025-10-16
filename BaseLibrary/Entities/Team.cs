using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Team : BaseEntity
    {
        public string? Club { get; set; }

        public ICollection<Athlete> Members { get; set; } = new List<Athlete>();
    }

}
