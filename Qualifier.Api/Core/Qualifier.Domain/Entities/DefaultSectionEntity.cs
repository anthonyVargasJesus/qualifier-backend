using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class DefaultSectionEntity : BaseEntity
    {
        public int defaultSectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public int documentTypeId { get; set; }
        public int companyId { get; set; }

        [NotMapped]
        public string numerationToShow { get; set; }
    }
}

