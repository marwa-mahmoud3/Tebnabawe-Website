
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tebnabawe.Data.Models
{
    [Table("Goals")]
    public class Goals 
    {
        public int Id { get; set; }
        public string GoalDescription { get; set; }
        public int AboutId { get; set; }
        [JsonIgnore, ForeignKey("AboutId")]
        public About AboutFk { get; set; }
    }
}
