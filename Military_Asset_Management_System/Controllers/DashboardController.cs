using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Military_Asset_Management_System.Data;
using System.Linq;

namespace Military_Asset_Management_System.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,BaseCommander,LogisticsOfficer")]
        [HttpGet("summary")]
        public IActionResult GetDashboardSummary()
        {
            var openingBalance = _context.Purchases.Sum(i => i.Quantity);
            var purchases = _context.Purchases.Sum(p => p.Quantity);
            var transferIn = _context.Transfers.Sum(t => t.ToBaseId != t.FromBaseId ? t.Quantity : 0);
            var transferOut = _context.Transfers.Sum(t => t.ToBaseId != t.FromBaseId ? t.Quantity : 0);
            var assigned = _context.Assignments.Sum(a => a.AssignedQuantity);
            var expended = _context.Assignments.Sum(e => e.ExpendedQuantity);

            var netMovement = purchases + transferIn - transferOut;
            var closingBalance = openingBalance + netMovement - (assigned + expended);

            var summary = new
            {
                OpeningBalance = openingBalance,
                Purchases = purchases,
                TransferIn = transferIn,
                TransferOut = transferOut,
                NetMovement = netMovement,
                Assigned = assigned,
                Expended = expended,
                ClosingBalance = closingBalance
            };

            return Ok(summary);
        }
    }
}
