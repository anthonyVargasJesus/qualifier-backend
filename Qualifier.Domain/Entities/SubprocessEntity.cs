using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class SubprocessEntity : BaseEntity
    {
        public int subprocessId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int macroprocessId { get; set; }
        public int companyId { get; set; }
        public MacroprocessEntity macroprocess { get; set; }
    }
}

