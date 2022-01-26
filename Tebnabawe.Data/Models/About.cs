using System.ComponentModel.DataAnnotations.Schema;

namespace Tebnabawe.Data.Models
{
    [Table("About")]
    public class About 
    {
        public int Id{ get; set; }
        public string AboutSheikh { get; set; }
        public string BriefAboutSheikh { get; set; }
        public string ImagePath { get; set; }
        public virtual string AdminId { get; set; }

        //[ForeignKey("AdminId")]
        //public AppUser UserFk { get; set; }
    }
}
