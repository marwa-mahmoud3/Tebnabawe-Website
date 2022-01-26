
using System.ComponentModel.DataAnnotations.Schema;

namespace Tebnabawe.Data.Models
{
    [Table("PhotosLibrary")]
    public class PhotosLibrary 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string ImagePath { get; set; }
    }
}
