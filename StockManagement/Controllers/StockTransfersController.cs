using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagement.Context;
using StockManagement.Models;

namespace StockManagement.Controllers
{
    public class StockTransfersController : Controller
    {
        private readonly AppDbContext _context;

        public StockTransfersController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: StockTransfers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StockTransfers.Include(s => s.FromStock).Include(s => s.Product).Include(s => s.ToStock);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StockTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTransfer = await _context.StockTransfers
                .Include(s => s.FromStock)
                .Include(s => s.Product)
                .Include(s => s.ToStock)
                .FirstOrDefaultAsync(m => m.StockTransferID == id);
            if (stockTransfer == null)
            {
                return NotFound();
            }

            return View(stockTransfer);
        }

        // GET: StockTransfers/Create
        public IActionResult Create()
        {
            ViewData["FromStockID"] = new SelectList(_context.Stocks, "StockID", "StockID");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");
            ViewData["ToStockID"] = new SelectList(_context.Stocks, "StockID", "StockID");
            return View();
        }

        // POST: StockTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockTransferID,ProductID,FromStockID,ToStockID,Count,Type")] StockTransfer stockTransfer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromStockID"] = new SelectList(_context.Stocks, "StockID", "StockID", stockTransfer.FromStockID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", stockTransfer.ProductID);
            ViewData["ToStockID"] = new SelectList(_context.Stocks, "StockID", "StockID", stockTransfer.ToStockID);
            return View(stockTransfer);
        }

        // GET: StockTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTransfer = await _context.StockTransfers.FindAsync(id);
            if (stockTransfer == null)
            {
                return NotFound();
            }
            ViewData["FromStockID"] = new SelectList(_context.Stocks, "StockID", "StockID", stockTransfer.FromStockID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", stockTransfer.ProductID);
            ViewData["ToStockID"] = new SelectList(_context.Stocks, "StockID", "StockID", stockTransfer.ToStockID);
            return View(stockTransfer);
        }

        // POST: StockTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockTransferID,ProductID,FromStockID,ToStockID,Count,Type")] StockTransfer stockTransfer)
        {
            if (id != stockTransfer.StockTransferID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTransferExists(stockTransfer.StockTransferID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromStockID"] = new SelectList(_context.Stocks, "StockID", "StockID", stockTransfer.FromStockID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", stockTransfer.ProductID);
            ViewData["ToStockID"] = new SelectList(_context.Stocks, "StockID", "StockID", stockTransfer.ToStockID);
            return View(stockTransfer);
        }

        // GET: StockTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTransfer = await _context.StockTransfers
                .Include(s => s.FromStock)
                .Include(s => s.Product)
                .Include(s => s.ToStock)
                .FirstOrDefaultAsync(m => m.StockTransferID == id);
            if (stockTransfer == null)
            {
                return NotFound();
            }

            return View(stockTransfer);
        }

        // POST: StockTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockTransfer = await _context.StockTransfers.FindAsync(id);
            _context.StockTransfers.Remove(stockTransfer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockTransferExists(int id)
        {
            return _context.StockTransfers.Any(e => e.StockTransferID == id);
        }
    }
}
