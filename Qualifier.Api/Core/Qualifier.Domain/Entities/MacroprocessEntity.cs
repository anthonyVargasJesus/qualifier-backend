using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class MacroprocessEntity : BaseEntity
    {
        public int macroprocessId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
    }
}

