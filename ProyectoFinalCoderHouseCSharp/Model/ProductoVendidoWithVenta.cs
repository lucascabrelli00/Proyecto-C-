namespace ProyectoFinalCoderHouseCSharp.Model
{
    public class ProductoVendidoWithVenta
    {
        public int VentaId { get; set; }
        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
    }
}
