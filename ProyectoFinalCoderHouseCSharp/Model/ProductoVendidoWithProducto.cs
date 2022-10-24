namespace ProyectoFinalCoderHouseCSharp.Model
{
    public class ProductoVendidoWithProducto
    {
        public int ProductoVendidoId { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
        public string Descripciones { get; set; }
        public int IdUsuario { get; set; }
    }
}
