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
    public class MotivoDevolucaosController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: MotivoDevolucaos
        public async Task<ActionResult> Index()
        {
            return View(await db.MotivoDevolucao.ToListAsync());
        }

        // GET: MotivoDevolucaos/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoDevolucao motivoDevolucao = await db.MotivoDevolucao.FindAsync(id);
            if (motivoDevolucao == null)
            {
                return HttpNotFound();
            }
            return View(motivoDevolucao);
        }

        // GET: MotivoDevolucaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MotivoDevolucaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Numero,Motivos")] MotivoDevolucao motivoDevolucao)
        {
            if (ModelState.IsValid)
            {
                if (!MotivoDevolucao.IsUnique(motivoDevolucao))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.MotivoDevolucao.Add(motivoDevolucao);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(motivoDevolucao);
        }

        // GET: MotivoDevolucaos/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoDevolucao motivoDevolucao = await db.MotivoDevolucao.FindAsync(id);
            if (motivoDevolucao == null)
            {
                return HttpNotFound();
            }
            return View(motivoDevolucao);
        }

        // POST: MotivoDevolucaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Numero,Motivos")] MotivoDevolucao motivoDevolucao)
        {
            if (ModelState.IsValid)
            {
                if (!MotivoDevolucao.IsUnique(motivoDevolucao))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.Entry(motivoDevolucao).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(motivoDevolucao);
        }

        // GET: MotivoDevolucaos/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoDevolucao motivoDevolucao = await db.MotivoDevolucao.FindAsync(id);
            if (motivoDevolucao == null)
            {
                return HttpNotFound();
            }
            return View(motivoDevolucao);
        }

        // POST: MotivoDevolucaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            MotivoDevolucao motivoDevolucao = await db.MotivoDevolucao.FindAsync(id);
            db.MotivoDevolucao.Remove(motivoDevolucao);
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
