using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Military_Asset_Management_System.Data;
using System.Linq;
using System;

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
        public IActionResult GetDashboardSummary([FromQuery] int? baseId, [FromQuery] int? equipmentTypeId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var purchases = _context.Purchases.AsQueryable();
            var transfers = _context.Transfers.AsQueryable();
            var assignments = _context.Assignments.AsQueryable();

            if (baseId.HasValue)
            {
                purchases = purchases.Where(p => p.BaseId == baseId);
                transfers = transfers.Where(t => t.ToBaseId == baseId || t.FromBaseId == baseId);
                assignments = assignments.Where(a => a.BaseId == baseId);
            }
            if (equipmentTypeId.HasValue)
            {
                purchases = purchases.Where(p => p.EquipmentTypeId == equipmentTypeId);
                transfers = transfers.Where(t => t.EquipmentTypeId == equipmentTypeId);
                assignments = assignments.Where(a => a.EquipmentTypeId == equipmentTypeId);
            }
            if (from.HasValue)
            {
                purchases = purchases.Where(p => p.PurchaseDate >= from);
                transfers = transfers.Where(t => t.TransferDate >= from);
                assignments = assignments.Where(a => a.AssignedDate >= from);
            }
            if (to.HasValue)
            {
                purchases = purchases.Where(p => p.PurchaseDate <= to);
                transfers = transfers.Where(t => t.TransferDate <= to);
                assignments = assignments.Where(a => a.AssignedDate <= to);
            }

            var openingBalance = purchases.Sum(i => i.Quantity);
            var purchasesSum = purchases.Sum(p => p.Quantity);
            var transferIn = transfers.Sum(t => t.ToBaseId != t.FromBaseId ? t.Quantity : 0);
            var transferOut = transfers.Sum(t => t.ToBaseId != t.FromBaseId ? t.Quantity : 0);
            var assigned = assignments.Sum(a => a.AssignedQuantity);
            var expended = assignments.Sum(e => e.ExpendedQuantity);

            var netMovement = purchasesSum + transferIn - transferOut;
            var closingBalance = openingBalance + netMovement - (assigned + expended);

            var summary = new
            {
                OpeningBalance = openingBalance,
                Purchases = purchasesSum,
                TransferIn = transferIn,
                TransferOut = transferOut,
                NetMovement = netMovement,
                Assigned = assigned,
                Expended = expended,
                ClosingBalance = closingBalance
            };

            return Ok(summary);
        }

        [Authorize(Roles = "Admin,BaseCommander,LogisticsOfficer")]
        [HttpGet("net-movement-details")]
        public IActionResult GetNetMovementDetails([FromQuery] int? baseId, [FromQuery] int? equipmentTypeId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            // Apply filters as above
            var purchases = _context.Purchases.AsQueryable();
            var transfers = _context.Transfers.AsQueryable();

            if (baseId.HasValue)
            {
                purchases = purchases.Where(p => p.BaseId == baseId);
                transfers = transfers.Where(t => t.ToBaseId == baseId || t.FromBaseId == baseId);
            }
            if (equipmentTypeId.HasValue)
            {
                purchases = purchases.Where(p => p.EquipmentTypeId == equipmentTypeId);
                transfers = transfers.Where(t => t.EquipmentTypeId == equipmentTypeId);
            }
            if (from.HasValue)
            {
                purchases = purchases.Where(p => p.PurchaseDate >= from);
                transfers = transfers.Where(t => t.TransferDate >= from);
            }
            if (to.HasValue)
            {
                purchases = purchases.Where(p => p.PurchaseDate <= to);
                transfers = transfers.Where(t => t.TransferDate <= to);
            }

            // Purchases
            var purchasesList = purchases
                .Select(p => new
                {
                    p.PurchaseId,
                    p.BaseId,
                    BaseName = p.Base.BaseName,
                    p.EquipmentTypeId,
                    EquipmentType = p.EquipmentType.Name,
                    p.Quantity,
                    p.PurchaseDate
                })
                .ToList();

            // Transfers In: assets received by the base
            var transferInList = transfers
                .Where(t => baseId == null || t.ToBaseId == baseId)
                .Select(t => new
                {
                    t.TransferId,
                    t.FromBaseId,
                    FromBaseName = t.FromBase.BaseName,
                    t.ToBaseId,
                    ToBaseName = t.ToBase.BaseName,
                    t.EquipmentTypeId,
                    EquipmentType = t.EquipmentType.Name,
                    t.Quantity,
                    t.TransferDate
                })
                .ToList();

            // Transfers Out: assets sent from the base
            var transferOutList = transfers
                .Where(t => baseId == null || t.FromBaseId == baseId)
                .Select(t => new
                {
                    t.TransferId,
                    t.FromBaseId,
                    FromBaseName = t.FromBase.BaseName,
                    t.ToBaseId,
                    ToBaseName = t.ToBase.BaseName,
                    t.EquipmentTypeId,
                    EquipmentType = t.EquipmentType.Name,
                    t.Quantity,
                    t.TransferDate
                })
                .ToList();

            return Ok(new
            {
                Purchases = purchasesList,
                TransferIn = transferInList,
                TransferOut = transferOutList
            });
        }

    }
}