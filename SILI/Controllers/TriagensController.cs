using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SILI.Models
{
    [Authorize]
    public class TriagensController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();
        private static long ColaboradorID;

        //// GET: Triagens
        //public async Task<ActionResult> Index()
        //{
        //    var triagem = db.Triagem.Include(t => t.CodigoPostal).Include(t => t.Morada).Include(t => t.User);
        //    return View(await triagem.ToListAsync());
        //}

        // GET: Triagens
        public async Task<ActionResult> Index(string Search, DateTime? Start, DateTime? End)
        {
            if (Request.Form["Export"] != null)
            {
                return DownloadExcel(Search, Start, End);
            }
            else
            {
                var triagem = db.Triagem.Include(t => t.CodigoPostal).Include(t => t.Morada).Include(t => t.User).Where(t => (Start == null || t.DataHoraRecepcao >= Start) && (End == null || t.DataHoraRecepcao <= End) && (Search == null || t.NrProcesso.Contains(Search) || t.User.FirstName.Contains(Search) || t.User.LastName.Contains(Search) || t.Morada.Nome.Contains(Search)));
                return View(await triagem.ToListAsync());
            }
        }

        public ActionResult DownloadExcel(string Search, DateTime? Start, DateTime? End)
        {
            List<ListagemTriagem> listx = (from pt in db.ProdutoTriagem 
                                            join t in db.Triagem on pt.TriagemID equals t.ID
                                            join dr in db.DetalheRecepcao on t.DetalheRecepcaoId equals dr.ID
                                            join r in db.Recepcao on dr.RecepcaoID equals r.ID
                                            where (Start == null || t.DataHoraRecepcao >= Start) && (End == null || t.DataHoraRecepcao <= End) && (Search == null || t.NrProcesso.Contains(Search) || t.User.FirstName.Contains(Search) || t.User.LastName.Contains(Search) || t.Morada.Nome.Contains(Search) )
                                            select new ListagemTriagem
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
                                                Devolvedor = dr.Morada.Nome,
                                                NrProcesso = t.NrProcesso,
                                                Data = t.DataHoraRecepcao,
                                                Hora = t.DataHoraRecepcao,
                                                HoraFimTriagem = new DateTime(1900,1,1),
                                                NIF = t.Morada.NIF,
                                                CodPostal = t.CodigoPostal.CodPostal,
                                                Nome = t.NomeMorada,
                                                DataGuia = t.DataGuia,
                                                NrGuia_NotaDevolucao = t.NrGuiaNotaDevol,
                                                SubUnidades = t.SubUnidades,
                                                EAN = pt.Produto.EAN,
                                                Ref = pt.Produto.Referencia,
                                                CodSecundario = pt.CodSecundario,
                                                Descricao = pt.Produto.Descricao,
                                                QtdDevolvida = pt.QtdDevolvida,
                                                Lote = pt.Lote,
                                                Validade = pt.Validade,
                                                Preco = pt.PVP,
                                                Tratamento = pt.Tratamento.Descricao,
                                                Localizacao = pt.Localizacao,
                                                MotivoDevolucao = pt.MotivoDevolucao.Motivos,
                                                Tipoogia = pt.Tipologia.Descricao,
                                                Obs = pt.Observacoes
                                            }).ToList();

            DataTable table = ExcelGenerator.ToDataTable(listx);

            byte[] data = ExcelGenerator.GenerateTriagemFile(table);

            Stream stream = new MemoryStream(data);

            Response.AddHeader("content-disposition", "attachment;  filename=Listagem_Triagem_" + DateTime.Now + ".xlsx");

            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
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
        public async Task<ActionResult> Create([Bind(Include = "ID,NrProcesso,DataHoraRecepcao,ColaboradorID,NIF,CodPostalID,NomeMorada,Localidade,NrGuiaNotaDevol,DataGuia,SubUnidades,DetalheRecepcaoId")] Triagem triagem)
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,NrProcesso,DataHoraRecepcao,ColaboradorID,NIF,CodPostalID,NomeMorada,Localidade,NrGuiaNotaDevol,DataGuia,SubUnidades,DetalheRecepcaoId")] Triagem triagem)
        {
            if (ModelState.IsValid)
            {
                Morada morada = db.Morada.Where(m => m.ID == triagem.NIF).FirstOrDefault();
                triagem.NomeMorada = morada == null ? string.Empty : morada.Nome;

                CodigoPostal cp = db.CodigoPostal.Where(c => c.ID == triagem.CodPostalID).FirstOrDefault();
                triagem.Localidade = cp == null ? string.Empty : cp.Localidade;

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
            if (db.ProdutoTriagem.Where(r => r.TriagemID == id).Count() > 0)
            {
                ModelState.AddModelError("", "Não é possivel apagar esta triagem, devido à mesma ter produtos associados.");
                Triagem tri = db.Triagem.Where(r => r.ID == id).FirstOrDefault();
                return View(tri);
            }

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
