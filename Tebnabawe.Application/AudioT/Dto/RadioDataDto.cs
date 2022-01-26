using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebnabawe.Application.AudioT.Dto
{
    public class RadioDataDto
    {
        public int Id { get; set; }
        public int AudioId { get; set; }
        public string AudioPath { get; set; }
        public int AudioLength { get; set; }
    }
}
