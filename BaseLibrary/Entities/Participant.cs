using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Participant : BaseEntity
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public double? SumPoints { get; set; }
        public int? Place {  get; set; }
    }
}
