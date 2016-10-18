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
    public class TipoDevolucaosController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: TipoDevolucaos
        public async Task<ActionResult> Index(string IsAvailable)
        {
            if (String.IsNullOrEmpty(IsAvailable))
            {
                return View(await db.TipoDevolucao.ToListAsync());
            }
            else
            {
                return View(await db.TipoDevolucao.Where(x => x.Disponivel == (IsAvailable == "True")).ToListAsync());
            }
        }

        // GET: TipoDevolucaos/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDevolucao tipoDevolucao = await db.TipoDevolucao.FindAsync(id);
            if (tipoDevolucao == null)
            {
                return HttpNotFound();
            }
            return View(tipoDevolucao);
        }

        // GET: TipoDevolucaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDevolucaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Numero,Descricao,Disponivel")] TipoDevolucao tipoDevolucao)
        {
            if (ModelState.IsValid)
            {
                if (!TipoDevolucao.IsUnique(tipoDevolucao))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.TipoDevolucao.Add(tipoDevolucao);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(tipoDevolucao);
        }

        // GET: TipoDevolucaos/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDevolucao tipoDevolucao = await db.TipoDevolucao.FindAsync(id);
            if (tipoDevolucao == null)
            {
                return HttpNotFound();
            }
            return View(tipoDevolucao);
        }

        // POST: TipoDevolucaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Numero,Descricao,Disponivel")] TipoDevolucao tipoDevolucao)
        {
            if (ModelState.IsValid)
            {
                if (!TipoDevolucao.IsUnique(tipoDevolucao))
                {
                    ModelState.AddModelError("Numero", "Number already exists.");
                }
                else
                {
                    db.Entry(tipoDevolucao).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(tipoDevolucao);
        }

        // GET: TipoDevolucaos/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDevolucao tipoDevolucao = await db.TipoDevolucao.FindAsync(id);
            if (tipoDevolucao == null)
            {
                return HttpNotFound();
            }
            return View(tipoDevolucao);
        }

        // POST: TipoDevolucaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            TipoDevolucao tipoDevolucao = await db.TipoDevolucao.FindAsync(id);
            db.TipoDevolucao.Remove(tipoDevolucao);
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
