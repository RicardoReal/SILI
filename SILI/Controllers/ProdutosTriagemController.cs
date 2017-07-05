using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.IO;

namespace SILI.Controllers
{
    [Authorize]
    public class ProdutosTriagemController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();
        private static long _triagemID;

        public ActionResult Download(long produtoTriagemId)
        {
            ProdutoTriagem produtoTriagem = db.ProdutoTriagem.Where(x => x.ID == produtoTriagemId).FirstOrDefault();

            Stream stream = new MemoryStream(FileGenerator.GenerateEtiqueta(produtoTriagem));

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + produtoTriagem.Produto.EAN + ".pdf");

            return new FileStreamResult(stream, "application/pdf");
        }

        // GET: ProdutosTriagem
        public async Task<ActionResult> Index()
        {
            var produtoTriagem = db.ProdutoTriagem.Include(p => p.MotivoDevolucao).Include(p => p.Produto).Include(p => p.Tratamento);
            return View(await produtoTriagem.ToListAsync());
        }

        public ActionResult GetProdutos(string query)
        {
            return Json(Produto.GetProdutos(query), JsonRequestBehavior.AllowGet);
        }

        // GET: ProdutosTriagem/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoTriagem produtoTriagem = await db.ProdutoTriagem.FindAsync(id);
            if (produtoTriagem == null)
            {
                return HttpNotFound();
            }
            return View(produtoTriagem);
        }

        // GET: ProdutosTriagem/Create
        public ActionResult Create(long TriagemID)
        {
            _triagemID = TriagemID;
            ViewBag.CreateLote = false;
            ViewBag.TriagemID = TriagemID;
            ViewBag.MotivoDevolucaoID = new SelectList(db.MotivoDevolucao, "ID", "Motivos");
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao");
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao");
            return View();
        }

        // POST: ProdutosTriagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TriagemID,EANCNP,QtdDevolvida,Lote,PVP,MotivoDevolucaoID,TratamentoID,Validade,TipologiaID,Localizacao")] ProdutoTriagem produtoTriagem)
        {

            ViewBag.CreateLote = false;

            if (ModelState.IsValid)
            {
                DateTime? validade = null;
                if (produtoTriagem.HasLote(produtoTriagem.Lote, produtoTriagem.EANCNP, out validade))
                {
                    produtoTriagem.Validade = validade;
                    produtoTriagem.TriagemID = _triagemID;
                    db.ProdutoTriagem.Add(produtoTriagem);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Edit", "ProdutosTriagem", new { id = produtoTriagem.ID });
                }
                else
                {
                    ViewBag.CreateLote = true;
                    ViewBag.EANCNP = produtoTriagem.EANCNP;
                    ModelState.AddModelError("", "Lote introduzido não existe para o produto indicado.");
                }
            }


            ViewBag.EANCNP = produtoTriagem.EANCNP;
            produtoTriagem.Produto = db.Produto.Where(p => p.ID == produtoTriagem.EANCNP).FirstOrDefault();
            ViewBag.MotivoDevolucaoID = new SelectList(db.MotivoDevolucao, "ID", "Motivos", produtoTriagem.MotivoDevolucaoID);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", produtoTriagem.TratamentoID);
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao", produtoTriagem.TipologiaID);

            return View(produtoTriagem);
        }

        // GET: ProdutosTriagem/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoTriagem produtoTriagem = await db.ProdutoTriagem.FindAsync(id);
            if (produtoTriagem == null)
            {
                return HttpNotFound();
            }

            ViewBag.CreateLote = false;
            ViewBag.MotivoDevolucaoID = new SelectList(db.MotivoDevolucao, "ID", "Motivos", produtoTriagem.MotivoDevolucaoID);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", produtoTriagem.TratamentoID);
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao", produtoTriagem.TipologiaID);
            return View(produtoTriagem);
        }

        // POST: ProdutosTriagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,TriagemID,EANCNP,QtdDevolvida,Lote,PVP,MotivoDevolucaoID,TratamentoID,Validade,TipologiaID,Localizacao")] ProdutoTriagem produtoTriagem)
        {
            ViewBag.CreateLote = false;
            if (ModelState.IsValid)
            {
                DateTime? validade = null;
                if (produtoTriagem.HasLote(produtoTriagem.Lote, produtoTriagem.EANCNP, out validade))
                {
                    produtoTriagem.Validade = validade;
                    produtoTriagem.TriagemID = _triagemID;
                    db.Entry(produtoTriagem).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    //return RedirectToAction("Edit", "ProdutosTriagem", new { id = produtoTriagem.ID });

                    if (Request.Form["Save"] != null)
                    {
                        return RedirectToAction("Edit", "Triagens", new { id = produtoTriagem.TriagemID });
                    }
                    else
                    {
                        return RedirectToAction("Create", "ProdutosTriagem", new { TriagemId = produtoTriagem.TriagemID });
                    }
                }
                else
                {
                    ViewBag.CreateLote = true;
                    ViewBag.EANCNP = produtoTriagem.EANCNP;
                    ModelState.AddModelError("", "Lote introduzido não existe para o produto indicado.");
                }

                
            }

            ViewBag.MotivoDevolucaoID = new SelectList(db.MotivoDevolucao, "ID", "Motivos", produtoTriagem.MotivoDevolucaoID);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", produtoTriagem.TratamentoID);
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao", produtoTriagem.TipologiaID);

            //return RedirectToAction("Create", "ProdutosTriagem", new { TriagemId = produtoTriagem.TriagemID });
            return View(produtoTriagem);
        }

        // GET: ProdutosTriagem/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoTriagem produtoTriagem = await db.ProdutoTriagem.FindAsync(id);
            if (produtoTriagem == null)
            {
                return HttpNotFound();
            }
            return View(produtoTriagem);
        }

        // POST: ProdutosTriagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ProdutoTriagem produtoTriagem = await db.ProdutoTriagem.FindAsync(id);
            db.ProdutoTriagem.Remove(produtoTriagem);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Triagens", new { id = produtoTriagem.TriagemID });
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
