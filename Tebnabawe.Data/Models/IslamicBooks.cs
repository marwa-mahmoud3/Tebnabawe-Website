
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tebnabawe.Data.Models
{
    [Table("IslamicBooks")]
    public class IslamicBooks 
    {
        public int Id { get; set; }
        public string BookPath { get; set; }
        public string BookImagePath { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
    }
}
