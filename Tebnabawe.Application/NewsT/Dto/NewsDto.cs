using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebnabawe.Application.NewsT.Dto
{
    public class NewsDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public string ImagePath { get; set; }
    }
}
