using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebnabawe.Application.Authentication.Dto
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string ClientURI { get; set; }
    }
}
