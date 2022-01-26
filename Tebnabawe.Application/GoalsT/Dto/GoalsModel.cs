using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebnabawe.Application.GoalsT.Dto
{
    public class GoalsModel
    {
        public int Id { get; set; }
        public string GoalDescription { get; set; }
        public int AboutId { get; set; }
    }
}
