using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Points
    {
        public int Id {  get; set; }
        public double? Amount { get; set; }
        public int RoundId { get; set; }
        public Round? Round { get; set; }
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }
    }
}
