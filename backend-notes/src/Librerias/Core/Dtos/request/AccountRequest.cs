using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.request
{
    public class AccountRequest
    {
        public int Id { get; set; }

        public required string User { get; set; }

        public required string Password { get; set; }
    }
}
