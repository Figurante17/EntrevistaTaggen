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
    public class RestaurantesController : Controller
    {
        private ContextRestaurante db = new ContextRestaurante();

        // GET: Restaurantes
        public ActionResult Index(String pesquisa = "")
        {
            var query = db.restaurantes.AsQueryable();
            if(!string.IsNullOrEmpty(pesquisa))
            {
                query = query.Where(r => r.nome.Contains(pesquisa));
            }
            query = query.OrderBy(r => r.nome);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurantes",query.ToList());
            }
            return View(query.ToList());
        }

        // GET: Restaurantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return View();
            }
            Restaurante restaurante = db.restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,nome")] Restaurante restaurante, int? id)
        {
            if (string.IsNullOrEmpty(restaurante.nome))
            {
                ModelState.AddModelError("nome", "Nome do restaurante é obrigatório");
            }
            else
            {
                if (id == null)
                {
                    db.restaurantes.Add(restaurante);
                    db.SaveChanges();
                    return RedirectToAction("Index").Mensagem("Restaurante adicionado com sucesso", "Sucesso");
                }
                else
                {
                    db.Entry(restaurante).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index").Mensagem("Restaurante alterado com sucesso" , "Sucesso");
                }
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nome")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = db.restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            db.restaurantes.Remove(restaurante);
            db.SaveChanges();
            var query = db.restaurantes.AsQueryable();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurantes", query.ToList()).Mensagem("Registro Apagado com Sucesso !", "Atenção");
            }           
            return RedirectToAction("Index").Mensagem("Registro Apagado com Sucesso !", "Atenção");
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurante restaurante = db.restaurantes.Find(id);
            db.restaurantes.Remove(restaurante);
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
