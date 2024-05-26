using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.request
{
    public class NoteRequest
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int IsArchive { get;set; }
        public required int IdAccount{get; set;}
        public required int IdTag { get; set;} 

    }
}
