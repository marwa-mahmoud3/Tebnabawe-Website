
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tebnabawe.Data.Models
{
    [Table("AudiosLibrary")]
    public class AudiosLibrary
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShiekhName { get; set; }
        public string AudioPath { get; set; }
    }
}
