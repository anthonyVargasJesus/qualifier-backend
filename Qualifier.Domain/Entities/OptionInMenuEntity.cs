using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class OptionInMenuEntity : BaseEntity
    {
        public int optionInMenuId { get; set; }
        public MenuEntity menu { get; set; }
        public OptionEntity option { get; set; }

        public int order { get; set; }

    }
}
