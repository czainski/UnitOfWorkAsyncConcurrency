using Microsoft.EntityFrameworkCore;
using UnitOfWorkAsync.Models;
using System.Data;
using System.Linq;
using UnitOfWorkAsync.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace UnitOfWorkAsync.Controllers
{
    public class TablesController : Controller
    {
        //
        TableRepository _repository;
        public TablesController() => 
               _repository = new UnitOfWork().TableRepository();
        
        // GET: Tables
        public async Task<ActionResult> Index()
        {
            Regex match = new Regex(@"^A[a-z]*");
            return View(await _repository.Select(orderBy: q => q.OrderByDescending(e => e.Name),
               filter: e => e.Name == match.Match(e.Name).Value));
        }
        // GET: Tables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            Table table; 
            if ((table = await _repository.Find(id)) == null)
                 return BadRequest();
            
            return View(table);
        }
        // GET: Tables/Create
        public ActionResult Create() => View();

        // POST: Tables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Table table)
        {
            if (ModelState.IsValid)
            {
                await _repository.Insert(table);
                return RedirectToAction("Index");
            }
            return View(table);
        }

        // GET: Tables/Edit/5
        public async Task<ActionResult> Edit(int? id) =>  await Details(id);

        // POST: Tables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Table table)
        {
            Table tableToUpdate = null;
            if (table.ID > 0 )
            {
                if ((tableToUpdate = await _repository.Find(table.ID)) == null)
                                 return BadRequest();
                //Updating data
                tableToUpdate.Name = table.Name;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.Update(tableToUpdate, table.RowVersion);
                    return RedirectToAction("Index");
                }
            }
             catch (DbUpdateConcurrencyException ex)
            {        
                 ModelState.AddModelError(string.Empty, "This record has already been modified!");
                 Debug.Write(ex.Source);
            }
            catch (DataException)
            {
                 ModelState.AddModelError(string.Empty, "Update is failed!");
            }
            return View(table);
        }
        // GET: Tables/Delete/5
        public async Task<ActionResult> Delete(int? id) => await Details(id);
        
        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (await _repository.Delete(id))
                       return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Delete is failed!");
            }
            return View(new Table());
        }
        //
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
