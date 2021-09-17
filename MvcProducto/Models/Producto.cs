using System;
using System.Data.Entity;

namespace MvcProducto.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Existencia { get; set; }
        public int NumPedidos { get; set; }
    }
    public class ProductoDBContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
    }
}