using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.response
{
    public class AccountResponse
    {
        public required string User { get; set; }

        public required string Password { get; set; }
    }
}
