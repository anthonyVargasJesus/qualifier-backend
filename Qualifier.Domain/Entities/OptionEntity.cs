using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class OptionEntity : BaseEntity
    {
        public int optionId { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public string url { get; set; }
        public bool? isMobile { get; set; }
        public int? companyId { get; set; }

        [NotMapped]
        public int order { get; set; }
        [NotMapped]
        public bool isChecked { get; set; }

        [NotMapped]
        public int optionInMenuInRoleId { get; set; }
    }
}

