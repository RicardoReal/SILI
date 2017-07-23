using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SILI.Models;

namespace SILI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RecepcaoController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Recepcao
        //public async Task<ActionResult> Index()
        //{
        //    var recepcao = db.Recepcao.Include(r => r.Morada).Include(r => r.User).OrderByDescending(r => r.ID);
        //    return View(await recepcao.ToListAsync());
        //}

        // GET: Recepcao
        public async Task<ActionResult> Index(string Search, DateTime? Start, DateTime? End)
        {
            if (Request.Form["Export"] != null)
            {
                return DownloadExcel(Search, Start, End);
            }
            else
            {
                var recepcao = db.Recepcao.Include(r => r.Morada).Include(r => r.User).OrderByDescending(r => r.ID).Where(x => (Start == null || x.DataHora >= Start) && (End == null || x.DataHora <= End) && (Search == null || x.NrRecepcao.Contains(Search) || x.User.FirstName.Contains(Search) || x.User.LastName.Contains(Search) || x.Morada.Nome.Contains(Search)));
                return View(await recepcao.ToListAsync());
            }
        }

        public ActionResult DownloadExcel(string Search, DateTime? Start, DateTime? End)
        {
            List<ListagemRecepcao> listx = (from dr in db.DetalheRecepcao
                                            join r in db.Recepcao on dr.RecepcaoID equals r.ID
                                            where (Start == null || r.DataHora >= Start) && (End == null || r.DataHora <= End) && (Search == null || r.NrRecepcao.Contains(Search) || r.User.FirstName.Contains(Search) || r.User.LastName.Contains(Search) || r.Morada.Nome.Contains(Search))
                                            select new ListagemRecepcao
                                            {
                                                NrRecepcao = r.NrRecepcao,
                                                DataHora = r.DataHora,
                                                HoraFim = new DateTime(1900, 1, 1),
                                                Colaborador = r.User.FirstName + " " + r.User.LastName,
                                                DataChegadaArmz = r.DataChegadaArmazem,
                                                HoraChegadaArmz = r.HoraChegadaArmazem,
                                                EntreguePor = r.Morada.Nome,
                                                Observacoes = r.Observacoes,
                                                NrVolumesRecepcionados = r.NrVolumesRecepcionados,
                                                DCR = r.DCR,
                                                NrDetalhe = dr.NrDetalhe,
                                                NrCliente = dr.Cliente.NrInterno,
                                                Cliente = dr.Cliente.Nome,
                                                NrVolumes = dr.NrVolumes,
                                                TipoDevolucao = dr.TipoDevolucao.Descricao,
                                                NReferencia = dr.NReferencia,
                                                NrGuiaTransporte = dr.NrGuiaTransporte,
                                                Devolvedor = dr.Morada.Nome
                                            }).ToList();

            DataTable table = ExcelGenerator.ToDataTable(listx);

            byte[] data = ExcelGenerator.GenerateRecepcaoFile(table);

            Stream stream = new MemoryStream(data);

            Response.AddHeader("content-disposition", "attachment;  filename=Listagem_Recepcao_" + DateTime.Now + ".xlsx");

            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult GetMoradas(string query)
        {
            return Json(Morada.GetMoradas(query), JsonRequestBehavior.AllowGet);
        }

        // GET: Recepcao/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcao recepcao = await db.Recepcao.FindAsync(id);
            if (recepcao == null)
            {
                return HttpNotFound();
            }
            return View(recepcao);
        }

        // GET: Recepcao/Create
        public ActionResult Create()
        {
            ViewBag.EntreguePor = new SelectList(db.Morada, "ID", "Nome");
            ViewBag.Colaborador = new SelectList(db.User, "ID", "FirstName");
            return View();
        }

        // POST: Recepcao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NrRecepcao,DataHora,DataChegadaArmazem,HoraChegadaArmazem,EntreguePor,Observacoes,NrVolumesRecepcionados,Colaborador,DCR")] Recepcao recepcao)
        {
            if (ModelState.IsValid)
            {
                recepcao.NrRecepcao = recepcao.GenerateNrRecepcao();
                recepcao.DataHora = DateTime.Now;
                recepcao.Colaborador = SILI.User.GetUserIdByUsername(User.Identity.Name);

                db.Recepcao.Add(recepcao);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = recepcao.ID });
            }

            ViewBag.EntreguePor = new SelectList(db.Morada, "ID", "Nome", recepcao.EntreguePor);
            ViewBag.Colaborador = new SelectList(db.User, "ID", "FirstName", recepcao.Colaborador);
            return View(recepcao);
        }

        // GET: Recepcao/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcao recepcao = await db.Recepcao.FindAsync(id);
            if (recepcao == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntreguePor = new SelectList(db.Morada, "ID", "Nome", recepcao.EntreguePor);
            ViewBag.Colaborador = new SelectList(db.User, "ID", "FirstName", recepcao.Colaborador);
            return View(recepcao);
        }

        public ActionResult GetDetalhesRecepcao(long id)
        {
            ViewData["RecepcaoId"] = id;
            return PartialView("DetalheRecepcaoList", db.DetalheRecepcao.Where(x => x.RecepcaoID == id).ToList());
        }

        // POST: Recepcao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NrRecepcao,DataHora,DataChegadaArmazem,HoraChegadaArmazem,EntreguePor,Observacoes,NrVolumesRecepcionados,Colaborador,UserID,DCR")] Recepcao recepcao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recepcao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EntreguePor = new SelectList(db.Morada, "ID", "Nome", recepcao.EntreguePor);
            ViewBag.Colaborador = new SelectList(db.User, "ID", "FirstName", recepcao.Colaborador);
            return View(recepcao);
        }

        // GET: Recepcao/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcao recepcao = await db.Recepcao.FindAsync(id);
            if (recepcao == null)
            {
                return HttpNotFound();
            }
            return View(recepcao);
        }

        // POST: Recepcao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            if (db.DetalheRecepcao.Where(r => r.RecepcaoID == id).Count() > 0)
            {
                ModelState.AddModelError("", "Não é possivel apagar esta recepção, devido à mesma ter Detalhes associados.");
                Recepcao rec = db.Recepcao.Where(r => r.ID == id).FirstOrDefault();
                return View(rec);
            }

            Recepcao recepcao = await db.Recepcao.FindAsync(id);
            db.Recepcao.Remove(recepcao);
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
