using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class OptionInMenuEntity : BaseEntity
    {
        public int optionInMenuId { get; set; }
        public int menuId { get; set; }
        public int optionId { get; set; }
        public int order { get; set; }
        public int? companyId { get; set; }
        public MenuEntity menu { get; set; }
        public OptionEntity option { get; set; }
    }
}
