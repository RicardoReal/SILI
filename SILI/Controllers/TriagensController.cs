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
using static SILI.Triagem;

namespace SILI.Models
{
    [Authorize]
    public class TriagensController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();
        private static long ColaboradorID;

        // GET: Triagens
        public async Task<ActionResult> Index()
        {
            var triagem = db.Triagem.Include(t => t.CodigoPostal).Include(t => t.Morada).Include(t => t.User);
            return View(await triagem.ToListAsync());
        }

        // GET: Triagens/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Triagem triagem = await db.Triagem.FindAsync(id);
            if (triagem == null)
            {
                return HttpNotFound();
            }
            return View(triagem);
        }

        // POST: Triagens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NrProcesso,DataHoraRecepcao,ColaboradorID,NIF,CodPostalID,NomeMorada,Localicade,NrGuiaNotaDevol,DataGuia,SubUnidades")] Triagem triagem)
        {
            if (ModelState.IsValid)
            {
                db.Triagem.Add(triagem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", "CodPostal", triagem.CodPostalID);
            ViewBag.NIF = new SelectList(db.Morada, "ID", "NIF", triagem.NIF);
           
            return View(triagem);
        }

        public ActionResult GetMoradas(string query)
        {
            return Json(Morada.GetMoradas(query), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCodPostais(string query)
        {
            return Json(CodigoPostal.GetCodPostais(query), JsonRequestBehavior.AllowGet);
        }

        // GET: Triagens/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Triagem triagem = await db.Triagem.FindAsync(id);
            if (triagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", "CodPostal", triagem.CodPostalID);
            ViewBag.NIF = new SelectList(db.Morada, "ID", "NIF", triagem.NIF);
            
            ColaboradorID = triagem.ColaboradorID;
            return View(triagem);
        }

        public ActionResult GetProdutosTriagem(long id)
        {
            ViewData["TriagemId"] = id;
            return PartialView("ProdutosTriagemList", db.ProdutoTriagem.Where(x => x.TriagemID == id).ToList());
        }

        // POST: Triagens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NrProcesso,DataHoraRecepcao,ColaboradorID,NIF,CodPostalID,NomeMorada,Localicade,NrGuiaNotaDevol,DataGuia,SubUnidades")] Triagem triagem)
        {
            if (ModelState.IsValid)
            {
                Morada morada = db.Morada.Where(m => m.ID == triagem.NIF).FirstOrDefault();
                triagem.NomeMorada = morada == null ? string.Empty : morada.Nome;

                CodigoPostal cp = db.CodigoPostal.Where(c => c.ID == triagem.CodPostalID).FirstOrDefault();
                triagem.Localidade = cp == null ? string.Empty : cp.Localidade ;

                triagem.ColaboradorID = ColaboradorID;

                db.Entry(triagem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                
            }
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", "CodPostal", triagem.CodPostalID);
            ViewBag.NIF = new SelectList(db.Morada, "ID", "NIF", triagem.NIF);
            ViewBag.ColaboradorID = new SelectList(db.User, "ID", "FirstName", triagem.ColaboradorID);
            
            return RedirectToAction("Edit", "Triagens", new { id = triagem.ID });
        }

        // GET: Triagens/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Triagem triagem = await db.Triagem.FindAsync(id);
            if (triagem == null)
            {
                return HttpNotFound();
            }
            return View(triagem);
        }

        // POST: Triagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Triagem triagem = await db.Triagem.FindAsync(id);
            db.Triagem.Remove(triagem);
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
