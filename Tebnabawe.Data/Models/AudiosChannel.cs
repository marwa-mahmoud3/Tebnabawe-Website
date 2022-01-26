
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tebnabawe.Data.Models
{
    [Table("AudiosChannel")]
    public class AudiosChannel 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShiekhName { get; set; }
        public string AudioPath { get; set; }
        public string Date { get; set; }
    }
}
