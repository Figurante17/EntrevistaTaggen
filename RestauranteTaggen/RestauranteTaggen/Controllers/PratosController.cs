using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestauranteTaggen.Context;
using RestauranteTaggen.Models;

namespace RestauranteTaggen.Controllers
{
    public class PratosController : Controller
    {
        private ContextRestaurante db = new ContextRestaurante();

        // GET: Pratos
        public ActionResult Index()
        {
            var q = (from p in db.Pratos
                     join r in db.restaurantes on p.codigoRestaurante equals r.codigo
                     select new { p.Nome, p.Preco, r.nome, p.CodigoPratos, r.codigo});

            var pratos = new List<PratosViewModel>();
            foreach (var p in q)
            {
                pratos.Add(new PratosViewModel()
                {
                    NomePrato = p.Nome,
                    Preco = p.Preco,
                    NomeRestaurante = p.nome,
                    CodigoPratos = p.CodigoPratos,
                    IdRestaurante = p.codigo
                 });
            }
            return View(pratos);

        }

        // GET: Pratos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return HttpNotFound();
            }
            return View(pratos);
        }

        // GET: Pratos/Create
        public ActionResult Create(int? id, int? IdRestaurante)
        {
            if (id == null)
            {
                ViewBag.RestauranteId = new SelectList(db.restaurantes, "codigo", "nome");
                return View();
            }
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestauranteId = new SelectList(db.restaurantes, "codigo", "nome", IdRestaurante);
            return View(pratos);

        }

        // POST: Pratos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoPratos,Nome,Preco,codigoRestaurante")] Pratos pratos, string RestauranteId, int? id)
        {

                if (id == null)
                {
                    pratos.codigoRestaurante = int.Parse(RestauranteId);
                    db.Pratos.Add(pratos);
                    db.SaveChanges();
                    return RedirectToAction("Index").Mensagem("Prato inserido com sucesso" , "Sucesso");
                }
                else
                {
                    pratos.codigoRestaurante = int.Parse(RestauranteId);
                    db.Entry(pratos).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index").Mensagem("Prato alterado com sucesso", "Sucesso");
            }
        }

        // GET: Pratos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return HttpNotFound();
            }
            return View(pratos);
        }

        // POST: Pratos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nome,preco")] Pratos pratos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pratos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pratos);
        }

        // GET: Pratos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return HttpNotFound();
            }
            db.Pratos.Remove(pratos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Pratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pratos pratos = db.Pratos.Find(id);
            db.Pratos.Remove(pratos);
            db.SaveChanges();
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
