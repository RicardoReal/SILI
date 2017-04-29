using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SILI;

namespace SILI.Controllers
{
    [Authorize]
    public class TarefasController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Tarefas
        public async Task<ActionResult> Index()
        {
            return View(await db.Tarefa.ToListAsync());
        }

        // GET: Tarefas/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = await db.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // GET: Tarefas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Numero,Descricao")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                if (!Tarefa.IsUnique(tarefa))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.Tarefa.Add(tarefa);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = await db.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Numero,Descricao")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                if (!Tarefa.IsUnique(tarefa))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.Entry(tarefa).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarefa tarefa = await db.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return HttpNotFound();
            }
            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Tarefa tarefa = await db.Tarefa.FindAsync(id);
            db.Tarefa.Remove(tarefa);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
