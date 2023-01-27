#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using IdentityByExamples.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace IdentityByExamples.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ApplicationContext _context;



        public TransactionController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Transactions.ToListAsync());
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(await _context.Transaction.Where(t => t.UserId == currentUserId).ToListAsync());
        }


        // GET: Transaction/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Transaction());
            else
                return View(_context.Transaction.Find(id));
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,RENT,ADDRESS,ROOM,BATHROOM")] Transaction transaction)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            

            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                {
                  
                    transaction.UserId = currentUserId;
                    _context.Add(transaction);
                }
                else
                {
                    if(currentUserId == transaction.UserId)
                    {
                        _context.Update(transaction);
                    }
                    
                }

                    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }
       


        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
