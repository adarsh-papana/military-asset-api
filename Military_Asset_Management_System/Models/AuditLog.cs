using System;

namespace Military_Asset_Management_System.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public string Action { get; set; } // e.g., "PurchaseCreated"
        public string EntityName { get; set; }
        public int? EntityId { get; set; }
        public string PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
        public string Details { get; set; }
    }
}