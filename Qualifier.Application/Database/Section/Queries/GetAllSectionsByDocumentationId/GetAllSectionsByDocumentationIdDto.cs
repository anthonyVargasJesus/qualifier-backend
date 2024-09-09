using Qualifier.Application.Database.Section.Queries.GetSectionsByDocumentationId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Section.Queries.GetAllSectionsByDocumentationId
{
    public class GetAllSectionsByDocumentationIdDto
    {
        public int sectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public string numerationToShow { get; set; }
        public List<GetAllSectionsByDocumentationIdDto> children { get; set; }
    }
}
