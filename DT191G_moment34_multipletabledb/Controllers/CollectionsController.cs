using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DT191G_moment34_multipletabledb.Data;
using DT191G_moment34_multipletabledb.Models;
using System.Drawing;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace DT191G_moment34_multipletabledb.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly CollectionContext _context;

        public CollectionsController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Collections
        public IActionResult Index2(string SearchString)
        {

            //get borrrowedlist & CollectionList
            var borrowedList = _context.Borrowed.Include(b => b.Collection).Include(b => b.Friend);
            var collectionList = _context.Collection;
            var friendList = _context.Friends;
            ViewData["CollectionId"] = new SelectList(_context.Collection, "CollectionId", "AlbumTitle");
            ViewData["FriendId"] = new SelectList(_context.Friends, "FriendId", "Name");

            foreach (var collection in collectionList)
            {
                foreach (var borrowed in borrowedList)
                {
                    if (collection.CollectionId == borrowed.CollectionId)
                    {
                        collection.Borrowed = borrowed.BorrowedId;
                        foreach (var friend in friendList)
                        {
                            if (friend.FriendId == borrowed.FriendId)
                            {
                                collection.Friend = friend.Name;
                            }
                        }
                    }
                }
            }

            //SEARCH Function
            //assign search string to viewdata
            ViewData["CurrentFilter"] = SearchString;

            //get all database in the case that SearchString is null
            var searchResult = from s in collectionList //LINQ method syntax // _context.Collection
                               select s;

            //in the case that SearchString not is null
            if (!String.IsNullOrEmpty(SearchString))
            {

                //LINQ Query using Query Syntax to seach all columns and fetch all according to search terms
                searchResult = from item in collectionList //Data Source //_context.Collection
                               where item.Artist.ToLower().Contains(SearchString.ToLower()) ||
                                    item.AlbumTitle.ToLower().Contains(SearchString.ToLower()) ||
                                    item.ReleaseYear.Contains(SearchString) ||
                                    item.SongList.ToLower().Contains(SearchString.ToLower()) //Condition
                               select item; //Selection


                //searchResult = searchResult.Where(s => s.AlbumTitle.Contains(SearchString)); //LINQ method syntax
            }

            return View(searchResult);
            //return View(collectionList);


        }




        //two other examples of code:

        //specific search
        //var year = _context.Collection.Where(c => c.ReleaseYear.Equals("2001")); 
        //return _context.Collection != null ? 
        //    View(await year.ToListAsync()) :
        //    Problem("Entity set 'CollectionContext.Collection'  is null.");

        // return whole table no search function
        //return _context.Collection != null ? 
        //    View(await _context.Collection.ToListAsync()) :
        //    Problem("Entity set 'CollectionContext.Collection'  is null.");



        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .FirstOrDefaultAsync(m => m.CollectionId == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollectionId,Artist,AlbumTitle,ReleaseYear,SongList")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index2));
            }
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            return View(collection);
        }

        // POST: Collections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollectionId,Artist,AlbumTitle,ReleaseYear,SongList")] Collection collection)
        {
            if (id != collection.CollectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.CollectionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index2));
            }
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .FirstOrDefaultAsync(m => m.CollectionId == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collection == null)
            {
                return Problem("Entity set 'CollectionContext.Collection'  is null.");
            }
            var collection = await _context.Collection.FindAsync(id);
            if (collection != null)
            {
                _context.Collection.Remove(collection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index2));
        }

        private bool CollectionExists(int id)
        {
          return (_context.Collection?.Any(e => e.CollectionId == id)).GetValueOrDefault();
        }


        
    }
}
