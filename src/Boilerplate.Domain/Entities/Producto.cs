using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities
{
    [Table("Producto", Schema = "public")]
    public class Producto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal Precio { get; set; }
    }
}
