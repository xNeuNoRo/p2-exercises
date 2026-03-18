using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Editorial
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}