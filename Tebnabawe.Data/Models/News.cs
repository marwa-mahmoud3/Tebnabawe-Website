
using System.ComponentModel.DataAnnotations.Schema;

namespace Tebnabawe.Data.Models
{
    [Table("News")]
    public class News 
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public string ImagePath { get; set; }
    }
}
