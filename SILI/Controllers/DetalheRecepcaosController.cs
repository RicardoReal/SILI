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
    public class DetalheRecepcaosController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: DetalheRecepcaos
        public async Task<ActionResult> Index()
        {
            var detalheRecepcao = db.DetalheRecepcao.Include(d => d.Cliente).Include(d => d.TipoDevolucao);
            return View(await detalheRecepcao.ToListAsync());
        }

        public ActionResult GetClientes(string query)
        {
            return Json(Cliente.GetClientes(query), JsonRequestBehavior.AllowGet);
        }

        // GET: DetalheRecepcaos/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalheRecepcao detalheRecepcao = await db.DetalheRecepcao.FindAsync(id);
            if (detalheRecepcao == null)
            {
                return HttpNotFound();
            }
            return View(detalheRecepcao);
        }

        //// GET: DetalheRecepcaos/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome");
        //    ViewBag.TipoRecepcaoId = new SelectList(db.TipoDevolucao, "ID", "Descricao");
        //    return View();
        //}

        // GET: DetalheRecepcaos/Create/Id
        public ActionResult Create(long RecepcaoId)
        {
            //ViewBag.RecepcaoID = new SelectList(db.Recepcao.Where(r => r.ID == RecepcaoId).ToList(), "ID");
            ViewBag.RecepcaoID = RecepcaoId;
            ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome");
            ViewBag.TipoRecepcaoId = new SelectList(db.TipoDevolucao, "ID", "Descricao");
            ViewBag.NrDetalhe = DetalheRecepcao.GenerateNrDetalheRecepcao(RecepcaoId);
            return View();
        }

        // POST: DetalheRecepcaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,RecepcaoID,NrDetalhe,ClienteId,NrVolumes,TipoRecepcaoId,NReferencia,RecepcaoId,NrGuiaTransporte")] DetalheRecepcao detalheRecepcao)
        {
            if (ModelState.IsValid)
            {
                Recepcao recep = db.Recepcao.Where(x => x.ID == detalheRecepcao.RecepcaoId).FirstOrDefault();

                recep.NrVolumesRecepcionados = db.DetalheRecepcao.Where(x => x.RecepcaoId == detalheRecepcao.RecepcaoId).AsEnumerable().Sum(r => r.NrVolumes);
                recep.NrVolumesRecepcionados += detalheRecepcao.NrVolumes;

                db.DetalheRecepcao.Add(detalheRecepcao);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Recepcao", new { id = detalheRecepcao.RecepcaoId });
            }

            //ViewBag.RecepcaoId = new SelectList(db.Recepcao.Where(r => r.ID == detalheRecepcao.RecepcaoId))
            ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome", detalheRecepcao.ClienteId);
            ViewBag.TipoRecepcaoId = new SelectList(db.TipoDevolucao, "ID", "Descricao", detalheRecepcao.TipoRecepcaoId);
            return View(detalheRecepcao);
        }

        // GET: DetalheRecepcaos/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalheRecepcao detalheRecepcao = await db.DetalheRecepcao.FindAsync(id);
            if (detalheRecepcao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome", detalheRecepcao.ClienteId);
            ViewBag.TipoRecepcaoId = new SelectList(db.TipoDevolucao, "ID", "Descricao", detalheRecepcao.TipoRecepcaoId);
            return View(detalheRecepcao);
        }

        // POST: DetalheRecepcaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,RecepcaoId,NrDetalhe,ClienteId,NrVolumes,TipoRecepcaoId,NReferencia,RecepcaoId,NrGuiaTransporte")] DetalheRecepcao detalheRecepcao)
        {
            if (ModelState.IsValid)
            {
                Recepcao recep = db.Recepcao.Where(x => x.ID == detalheRecepcao.RecepcaoId).FirstOrDefault();

                recep.NrVolumesRecepcionados = db.DetalheRecepcao.Where(x => x.RecepcaoId == detalheRecepcao.RecepcaoId && x.ID != detalheRecepcao.ID).AsEnumerable().Sum(r => r.NrVolumes);
                recep.NrVolumesRecepcionados += detalheRecepcao.NrVolumes;

                db.Entry(detalheRecepcao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Recepcao", new { id = detalheRecepcao.RecepcaoId });
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome", detalheRecepcao.ClienteId);
            ViewBag.TipoRecepcaoId = new SelectList(db.TipoDevolucao, "ID", "Descricao", detalheRecepcao.TipoRecepcaoId);
            return View(detalheRecepcao);
        }

        // GET: DetalheRecepcaos/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalheRecepcao detalheRecepcao = await db.DetalheRecepcao.FindAsync(id);
            if (detalheRecepcao == null)
            {
                return HttpNotFound();
            }
            return View(detalheRecepcao);
        }

        // POST: DetalheRecepcaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            DetalheRecepcao detalheRecepcao = await db.DetalheRecepcao.FindAsync(id);
            db.DetalheRecepcao.Remove(detalheRecepcao);

            Recepcao recep = db.Recepcao.Where(x => x.ID == detalheRecepcao.RecepcaoId).FirstOrDefault();

            recep.NrVolumesRecepcionados = db.DetalheRecepcao.Where(x => x.RecepcaoId == detalheRecepcao.RecepcaoId).AsEnumerable().Sum(r => r.NrVolumes);
            recep.NrVolumesRecepcionados -= detalheRecepcao.NrVolumes;

            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Recepcao", new { id = detalheRecepcao.RecepcaoId });
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
