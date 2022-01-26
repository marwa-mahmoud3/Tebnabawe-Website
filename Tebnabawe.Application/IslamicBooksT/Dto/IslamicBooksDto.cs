using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebnabawe.Application.IslamicBooksT.Dto
{
    public class IslamicBooksDto
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
