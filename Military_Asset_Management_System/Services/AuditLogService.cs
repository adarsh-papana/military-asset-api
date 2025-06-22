using Military_Asset_Management_System.Data;
using Military_Asset_Management_System.Models;

public class AuditLogService
{
    private readonly AppDbContext _context;
    public AuditLogService(AppDbContext context) => _context = context;

    public async Task LogAsync(string action, string entity, int? entityId, string user, string details)
    {
        var log = new AuditLog
        {
            Action = action,
            EntityName = entity,
            EntityId = entityId,
            PerformedBy = user,
            Details = details
        };
        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}