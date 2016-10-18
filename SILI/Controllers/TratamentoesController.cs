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
    public class TratamentoesController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Tratamentoes
        public async Task<ActionResult> Index()
        {
            return View(await db.Tratamento.ToListAsync());
        }

        // GET: Tratamentoes/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamento tratamento = await db.Tratamento.FindAsync(id);
            if (tratamento == null)
            {
                return HttpNotFound();
            }
            return View(tratamento);
        }

        // GET: Tratamentoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tratamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Numero,Descricao")] Tratamento tratamento)
        {
            if (ModelState.IsValid)
            {
                if (!Tratamento.IsUnique(tratamento))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.Tratamento.Add(tratamento);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(tratamento);
        }

        // GET: Tratamentoes/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamento tratamento = await db.Tratamento.FindAsync(id);
            if (tratamento == null)
            {
                return HttpNotFound();
            }
            return View(tratamento);
        }

        // POST: Tratamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Numero,Descricao")] Tratamento tratamento)
        {
            if (ModelState.IsValid)
            {
                if (!Tratamento.IsUnique(tratamento))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.Entry(tratamento).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(tratamento);
        }

        // GET: Tratamentoes/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tratamento tratamento = await db.Tratamento.FindAsync(id);
            if (tratamento == null)
            {
                return HttpNotFound();
            }
            return View(tratamento);
        }

        // POST: Tratamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Tratamento tratamento = await db.Tratamento.FindAsync(id);
            db.Tratamento.Remove(tratamento);
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
