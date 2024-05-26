using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.response
{
    public class NoteResponse
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Account { get; set; }
        public required int IsArchive { get; set; }
        public required string Tag { get; set; }
    }
}
