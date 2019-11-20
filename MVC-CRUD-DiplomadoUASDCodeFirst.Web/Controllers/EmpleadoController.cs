using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using MVC_CRUD_DiplomadoUASDCodeFirst.Model.DAL;
using MVC_CRUD_DiplomadoUASDCodeFirst.Model.Models;

namespace MVC_CRUD_DiplomadoUASDCodeFirst.Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private EmpleadoContext db = new EmpleadoContext();

        // GET: Empleado
        public ActionResult Index()
        {
            return View(db.Empleado.ToList());
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpleadoID,Nombres,Apellidos,Fecha_Ingreso")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpleadoID,Nombres,Apellidos,Fecha_Ingreso")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            db.Empleado.Remove(empleado);
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

        public FileResult ExportarEmpleadosCVS()
        {
            //Creamo un objeto objEmpleado de tipo EmpleadoContext
            EmpleadoContext objEmpleado = new EmpleadoContext();

            //Indicamos las columnas que tendra el archivo generado por la accion FileResult
            DataTable dt = new DataTable("Grid");

            dt.Columns.AddRange(new DataColumn[4] {new DataColumn("Codigo"),
                                        new DataColumn("Nombres"),
                                        new DataColumn("Apellidos"),
                                         new DataColumn("Fecha_Ingreso")});
            //Sentencia LINQ para obtenemos los 3 primeros 
            /*var empleado = from Empleado in objEmpleado.Empleado.Take(3)
                           select Empleado;*/


            //Consulta que muestra todos los empleados con nombre que empiecen con J
           /* var empleado = from Empleado in objEmpleado.Empleado
                           where Empleado.Nombres.StartsWith("J") 
                           select Empleado;*/


            //Consulta que muestra todos los empleados con apellidos que empiecen con A
            var empleado = from Empleado in objEmpleado.Empleado
                           where Empleado.Apellidos.Contains("A")
                           select Empleado;

            //Recoremos el objeto empleado y agragamos cada fila al archivo que se ha generado
            foreach (var Empleado in empleado)
            {
                dt.Rows.Add(Empleado.EmpleadoID, Empleado.Nombres, Empleado.Apellidos, Empleado.Fecha_Ingreso);
            }
            using (XLWorkbook wb= new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
              using (System.IO.MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }
    
}
