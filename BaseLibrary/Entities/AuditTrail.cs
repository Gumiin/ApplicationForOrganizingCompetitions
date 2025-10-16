using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class AuditTrail : BaseEntity
    {
        public int ResultId { get; set; }
        public Result Result { get; set; } = default!;

        public string ActionType { get; set; } = string.Empty;      // "CREATE", "UPDATE", "DELETE"
        public string? ChangedBy { get; set; }                      // np. ID lub e-mail użytkownika
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public string? FieldName { get; set; }                      // np. "Score"
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }

}
