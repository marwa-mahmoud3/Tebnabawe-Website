
using System.ComponentModel.DataAnnotations.Schema;

namespace Tebnabawe.Data.Models
{
    [Table("VideosChannel")]
    public class VideosChannel 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShiekhName { get; set; }
        public string Description { get; set; }
        public string VideoPath { get; set; }
    }
}
