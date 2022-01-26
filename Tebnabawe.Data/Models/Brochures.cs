
using System.ComponentModel.DataAnnotations.Schema;

namespace Tebnabawe.Data.Models
{
    [Table("Brochures")]
    public class Brochures 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePathFirst { get; set; }
        public string ImagePathSecond { get; set; }
    }
}
