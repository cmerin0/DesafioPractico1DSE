using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcProducto.Models;

namespace MvcProducto.Controllers
{
    public class ProductosController : Controller
    {
        private ProductoDBContext db = new ProductoDBContext();

        // GET: Productos
        public ActionResult Index(string descripcion, string existencia, string precioventa)
        {
            var productos = from p in db.Productos select p;
            if (!String.IsNullOrEmpty(descripcion))
            {
                productos = productos.Where(s => s.Descripcion.Contains(descripcion));
            }
            if (!String.IsNullOrEmpty(existencia))
            {
                int exst = Convert.ToInt32(existencia);
                productos = productos.Where(s => s.Existencia == exst);
            }
            if (!String.IsNullOrEmpty(precioventa))
            {
                decimal pventa = Convert.ToDecimal(precioventa);
                productos = productos.Where(s => s.PrecioVenta.Equals(pventa));
            }
            return View(productos);

        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IdProducto,Descripcion,Categoria,Costo,PrecioVenta,Existencia,NumPedidos")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IdProducto,Descripcion,Categoria,Costo,PrecioVenta,Existencia,NumPedidos")] Producto producto)
        {
            if(producto.NumPedidos > producto.Existencia)
            {
                ModelState.AddModelError("", "Error: Numero de pedidos excede a existencias");
            }
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
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
