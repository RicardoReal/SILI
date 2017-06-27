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
using System.Data.SqlClient;

namespace SILI.Controllers
{
    [Authorize]
    public class TipologiasController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Tipologias
        public async Task<ActionResult> Index()
        {
            return View(await db.Tipologia.ToListAsync());
        }

        // GET: Tipologias/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia tipologia = await db.Tipologia.FindAsync(id);
            if (tipologia == null)
            {
                return HttpNotFound();
            }
            return View(tipologia);
        }

        // GET: Tipologias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipologias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Numero,Descricao")] Tipologia tipologia)
        {
            if (ModelState.IsValid)
            {
                if (!Tipologia.IsUnique(tipologia))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.Tipologia.Add(tipologia);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(tipologia);
        }

        // GET: Tipologias/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia tipologia = await db.Tipologia.FindAsync(id);
            if (tipologia == null)
            {
                return HttpNotFound();
            }
            return View(tipologia);
        }

        // POST: Tipologias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Numero,Descricao")] Tipologia tipologia)
        {
            if (ModelState.IsValid)
            {
                if (!Tipologia.IsUnique(tipologia))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.Entry(tipologia).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(tipologia);
        }

        // GET: Tipologias/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipologia tipologia = await db.Tipologia.FindAsync(id);
            if (tipologia == null)
            {
                return HttpNotFound();
            }
            return View(tipologia);
        }

        // POST: Tipologias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Tipologia tipologia = await db.Tipologia.FindAsync(id);
            try
            {
                db.Tipologia.Remove(tipologia);
                await db.SaveChangesAsync();
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "Não é possivel apagar tipologias que estejam a ser referenciados.");
                return View(tipologia);
            }
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
