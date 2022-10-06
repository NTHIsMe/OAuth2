using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockProject.Models
{
    public class TwoFactorAuthenticationViewModel
    {
        public string Code { get; set; }

        public string Token { get; set; }
        public string QRCodeUrl { get; set; }
    }
}
