using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Round : BaseEntity
    {
        public int CompetitionId { get; set; }
        public Competition? Competition { get; set; }
    }
}
