﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_CRUD_DiplomadoUASDCodeFirst.Model.DAL;
using MVC_CRUD_DiplomadoUASDCodeFirst.Model.Models;

namespace MVC_CRUD_DiplomadoUASDCodeFirst.Web.Controllers
{
    public class RegistroController : Controller
    {
        private EmpleadoContext db = new EmpleadoContext();

        // GET: Registro
        public ActionResult Index()
        {
            var registro = db.Registro.Include(r => r.Departamento).Include(r => r.Empleado);
            return View(registro.ToList());
        }

        // GET: Registro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registro.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            return View(registro);
        }

        // GET: Registro/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "DepartamentoID", "Descripcion");
            ViewBag.EmpleadoID = new SelectList(db.Empleado, "EmpleadoID", "Nombres");
            return View();
        }

        // POST: Registro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistroID,Sueldo,DepartamentoID,EmpleadoID")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                db.Registro.Add(registro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoID = new SelectList(db.Departamento, "DepartamentoID", "Descripcion", registro.DepartamentoID);
            ViewBag.EmpleadoID = new SelectList(db.Empleado, "EmpleadoID", "Nombres", registro.EmpleadoID);
            return View(registro);
        }

        // GET: Registro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registro.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "DepartamentoID", "Descripcion", registro.DepartamentoID);
            ViewBag.EmpleadoID = new SelectList(db.Empleado, "EmpleadoID", "Nombres", registro.EmpleadoID);
            return View(registro);
        }

        // POST: Registro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistroID,Sueldo,DepartamentoID,EmpleadoID")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "DepartamentoID", "Descripcion", registro.DepartamentoID);
            ViewBag.EmpleadoID = new SelectList(db.Empleado, "EmpleadoID", "Nombres", registro.EmpleadoID);
            return View(registro);
        }

        // GET: Registro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registro registro = db.Registro.Find(id);
            if (registro == null)
            {
                return HttpNotFound();
            }
            return View(registro);
        }

        // POST: Registro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registro registro = db.Registro.Find(id);
            db.Registro.Remove(registro);
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
