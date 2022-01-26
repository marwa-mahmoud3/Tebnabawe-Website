
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tebnabawe.Data.Models
{
    [Table("Works")]
    public class Works 
    {
        public int Id { get; set; }
        public string WorkDescription { get; set; }
        public int AboutId { get; set; }

        [JsonIgnore, ForeignKey("AboutId")]
        public About About { get; set; }
    }
}
