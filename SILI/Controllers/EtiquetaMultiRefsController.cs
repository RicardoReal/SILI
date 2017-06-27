using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SILI.Models;
using System.Data.Entity;
using System.IO;

namespace SILI.Controllers
{
    [Authorize]
    public class EtiquetaMultiRefsController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();
        private static string _nrDetalhe;

        public ActionResult Create(long TriagemId)
        {
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao");
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao");
            ViewBag.TriagemID = TriagemId;

            _nrDetalhe = db.Triagem.Where(t => t.ID == TriagemId).Include(x => x.DetalheRecepcao).FirstOrDefault().DetalheRecepcao.NrDetalhe;
            return View();
        }

        // POST: NovaTriagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NrDetalhe,Quantidade,Localizacao,TipologiaID,TratamentoID")] EtiquetaMultiRef etiquetaMultiRef)
        {
            if (ModelState.IsValid)
            {
                etiquetaMultiRef.Tratamento = db.Tratamento.Where(t => t.ID == etiquetaMultiRef.TratamentoID).FirstOrDefault();
                etiquetaMultiRef.Tipologia = db.Tipologia.Where(t => t.ID == etiquetaMultiRef.TipologiaID).FirstOrDefault();
                etiquetaMultiRef.NrDetalhe = _nrDetalhe;

                long userId = SILI.User.GetUserIdByUsername(User.Identity.Name);
                User user = db.User.Where(x => x.ID == userId).FirstOrDefault();
                Stream stream = new MemoryStream(FileGenerator.GenerateEtiqueta(etiquetaMultiRef, user.FormattedToString));

                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + etiquetaMultiRef.NrDetalhe + "_MultiRef.pdf");

                return new FileStreamResult(stream, "application/pdf");

            }
            ModelState.AddModelError("", "Por favor, confirme os campos introduzidos.");
            ViewBag.TratamentoID = etiquetaMultiRef.TratamentoID;
            ViewBag.TipologiaID = etiquetaMultiRef.TipologiaID;

            return View(etiquetaMultiRef);
        }
    }
}