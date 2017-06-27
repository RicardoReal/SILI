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
    public class MoradasController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Moradas
        public async Task<ActionResult> Index()
        {
            var morada = db.Morada.Include(m => m.CodigoPostal).Include(m => m.TipoDevolvedor);
            return View(await morada.ToListAsync());
        }

        public ActionResult GetCodPostais(string query)
        {
            return Json(CodigoPostal.GetCodPostais(query), JsonRequestBehavior.AllowGet);
        }

        // GET: Moradas/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Morada morada = await db.Morada.FindAsync(id);
            if (morada == null)
            {
                return HttpNotFound();
            }
            return View(morada);
        }

        // GET: Moradas/Create
        public ActionResult Create()
        {
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", "CodPostal");
            ViewBag.TipoDevolvedorID = new SelectList(db.TipoDevolvedor, "ID", "Descricao");
            return View();
        }

        // POST: Moradas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NIF,Nome,CodPostalID,Morada1,Telefone,NomeContacto,TipoDevolvedorID")] Morada morada)
        {
            if (ModelState.IsValid)
            {
                db.Morada.Add(morada);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", "CodPostal", morada.CodPostalID);
            ViewBag.TipoDevolvedorID = new SelectList(db.TipoDevolvedor, "ID", "Descricao", morada.TipoDevolvedorID);
            return View(morada);
        }

        // GET: Moradas/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Morada morada = await db.Morada.FindAsync(id);
            if (morada == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", "CodPostal", morada.CodPostalID);
            ViewBag.TipoDevolvedorID = new SelectList(db.TipoDevolvedor, "ID", "Descricao", morada.TipoDevolvedorID);
            return View(morada);
        }

        // POST: Moradas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NIF,Nome,CodPostalID,Morada1,Telefone,NomeContacto,TipoDevolvedorID")] Morada morada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(morada).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", "CodPostal", morada.CodPostalID);
            ViewBag.TipoDevolvedorID = new SelectList(db.TipoDevolvedor, "ID", "Descricao", morada.TipoDevolvedorID);
            return View(morada);
        }

        // GET: Moradas/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Morada morada = await db.Morada.FindAsync(id);
            if (morada == null)
            {
                return HttpNotFound();
            }
            return View(morada);
        }

        // POST: Moradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Morada morada = await db.Morada.FindAsync(id);
            try
            {
                db.Morada.Remove(morada);
                await db.SaveChangesAsync();
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", "Não é possivel apagar moradas que estejam a ser referenciadas.");
                return View(morada);
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
