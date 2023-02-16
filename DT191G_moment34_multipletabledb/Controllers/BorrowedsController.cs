using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DT191G_moment34_multipletabledb.Data;
using DT191G_moment34_multipletabledb.Models;
using System.ComponentModel.DataAnnotations;

namespace DT191G_moment34_multipletabledb.Controllers
{
    public class BorrowedsController : Controller
    {
        private readonly CollectionContext _context;

        public BorrowedsController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Borroweds
        public async Task<IActionResult> Index()
        {
            var collectionContext = _context.Borrowed.Include(b => b.Collection).Include(b => b.Friend);
            return View(await collectionContext.ToListAsync());
        }

        // GET: Borroweds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Borrowed == null)
            {
                return NotFound();
            }

            var borrowed = await _context.Borrowed
                .Include(b => b.Collection)
                .Include(b => b.Friend)
                .FirstOrDefaultAsync(m => m.BorrowedId == id);
            if (borrowed == null)
            {
                return NotFound();
            }

            return View(borrowed);
        }

        // GET: Borroweds/Create
        public IActionResult Create()  
        {
            ViewData["CollectionId"] = new SelectList(_context.Collection, "CollectionId", "AlbumTitle");
            ViewData["FriendId"] = new SelectList(_context.Friends, "FriendId", "Name");
            return View();
        }

        // POST: Borroweds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowedId,Date,CollectionId,FriendId")] Borrowed borrowed)
        {

            _context.Add(borrowed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Borroweds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Borrowed == null)
            {
                return NotFound();
            }

            var borrowed = await _context.Borrowed.FindAsync(id);
            if (borrowed == null)
            {
                return NotFound();
            }
            ViewData["CollectionId"] = new SelectList(_context.Collection, "CollectionId", "AlbumTitle", borrowed.CollectionId);
            ViewData["FriendId"] = new SelectList(_context.Friends, "FriendId", "Name", borrowed.FriendId);
            return View(borrowed);
        }

        // POST: Borroweds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowedId,Date,CollectionId,FriendId")] Borrowed borrowed)
        {
            if (id != borrowed.BorrowedId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(borrowed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedExists(borrowed.BorrowedId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}

            ViewData["CollectionId"] = new SelectList(_context.Collection, "CollectionId", "AlbumTitle", borrowed.CollectionId);
            ViewData["FriendId"] = new SelectList(_context.Friends, "FriendId", "Name", borrowed.FriendId);
            //return View(borrowed);
            return RedirectToAction(nameof(Index));
        }

        // GET: Borroweds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Borrowed == null)
            {
                return NotFound();
            }

            var borrowed = await _context.Borrowed
                .Include(b => b.Collection)
                .Include(b => b.Friend)
                .FirstOrDefaultAsync(m => m.BorrowedId == id);
            if (borrowed == null)
            {
                return NotFound();
            }

            return View(borrowed);
        }

        // POST: Borroweds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Borrowed == null)
            {
                return Problem("Entity set 'CollectionContext.Borrowed'  is null.");
            }
            var borrowed = await _context.Borrowed.FindAsync(id);
            if (borrowed != null)
            {
                _context.Borrowed.Remove(borrowed);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedExists(int id)
        {
          return (_context.Borrowed?.Any(e => e.BorrowedId == id)).GetValueOrDefault();
        }
    }
}
